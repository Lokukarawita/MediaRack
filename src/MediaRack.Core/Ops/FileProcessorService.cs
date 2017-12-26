using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Configuration;
using MediaRack.Core.Util.Strings;
using MediaRack.Core.Api.TMDDb;
using MediaRack.Core.Api.Mapping;
using System.IO;
using MediaRack.Core.Data.Common;
using MediaRack.Core.Util.Globalization;

namespace MediaRack.Core.Ops
{
    public class FileProcessorService : IDisposable
    {
        private log4net.ILog log;
        private Timer queueCheck;
        private bool processing;
        private LanguageCode defaultCode;

        public FileProcessorService()
        {
            queueCheck = new Timer();
            queueCheck.Elapsed += queueCheck_Elapsed;

            var interval = ConfigKeys.KEY_FILPROC_QUEUECHECKINTERVAL.GetConfigValue<int>(60);
            queueCheck.Interval = TimeSpan.FromSeconds(interval).TotalMilliseconds;

            defaultCode = LanguageCode.DefaultCode();

            queueCheck.Start();

            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }


        private void queueCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (processing)
                return;

            processing = true;
            MediaRack.Core.Data.Common.LocalFileQueueItem file = null;
            try
            {
                file = Data.Local.LocalFileQueue.Instance.TakeFile();
                if (file != null)
                {
                    if (file.ContentType == Data.Common.MediaClassification.Movie || file.ContentType == Data.Common.MediaClassification.RMovie)
                    {
                        try
                        {
                            ProcessMovieEntry(file);
                            Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, false);
                        }
                        catch (Exception)
                        {
                            Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, true);
                        }
                    }
                    else
                    {
                        //TV shows
                    }
                }
            }
            catch (Exception)
            {
                if (file != null)
                {
                    Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, true);
                }
            }

            processing = false;
        }

        //Movie Processing

        private void ProcessMovieEntry(MediaRack.Core.Data.Common.LocalFileQueueItem file)
        {

            //Data
            string d_moviename = string.Empty;
            int d_movieyear = default(int);
            WatTmdb.V3.MovieResult d_serchResult = null;
            MediaRack.Core.Data.Common.MediaEntry d_localEntry = null;
            WatTmdb.V3.TmdbMovie d_tmdbmovie = null;

            //Pattern extraction
            if (!string.IsNullOrWhiteSpace(file.FolderPattern))
            {
                var ptrn_data = file.FilePath.ExtractDataByFolder(file.FolderPattern);

                if (ptrn_data.Count > 0)
                {
                    if (ptrn_data.Keys.Contains("|moviename|"))
                        d_moviename = ptrn_data["|moviename|"];
                    if (ptrn_data.Keys.Contains("|year|"))
                    {
                        var year_str = ptrn_data["|year|"];
                        if (!int.TryParse(year_str, out d_movieyear)) { d_movieyear = default(int); }
                    }
                }
            }
            else
            {
                //File name extraction
                var fileName = Path.GetFileNameWithoutExtension(file.FilePath);
                var extracted = fileName.ExtractDataByFileName();

                d_moviename = extracted["movie_name"];
                try
                {
                    d_movieyear = int.Parse(extracted["movie_year"]);
                }
                catch (Exception)
                {
                    d_movieyear = default(int);
                }
            }

            //Error check
            if (string.IsNullOrWhiteSpace(d_moviename))
            {
                throw new FileProcessingException("Extracted movie name is empty");
            }

            //Find movie info
            var tmdb_search = TmdbClient.Tmdb.SearchMovie(d_moviename, 1);
            if (tmdb_search.results != null)
            {
                if (tmdb_search.total_results == 1)
                {
                    d_serchResult = tmdb_search.results.First();
                }
                else
                {
                    d_serchResult = tmdb_search.results.FirstOrDefault(x => x.release_date.Contains(d_movieyear.ToString()));
                }
            }

            //Error check
            if (d_serchResult == null)
            {
                throw new FileProcessingException("TMDB search for '" + d_moviename + "' returned empty results");
            }

            //Get Movie info
            if (d_serchResult != null)
            {
                LocalDataStore store = new LocalDataStore();
                d_localEntry = store.GetByTmdbId(d_serchResult.id);

                if (d_localEntry == null)
                {
                    d_tmdbmovie = TmdbClient.Tmdb.GetMovieInfo(d_serchResult.id);
                }
            }

            //Error check
            if (d_localEntry == null && d_tmdbmovie == null)
            {
                throw new FileProcessingException("Cannot find a matching media entry for for '" + d_moviename + "' ('" + file.MD5 + "') in local storage or in TMDB");
            }


            if (d_localEntry != null)
            {
                //File info
                var fileInfo = FillFileMetaInfo(file.FilePath, file.MD5);
                if (d_localEntry.FileInfo != null)
                {
                    var exiting_file = d_localEntry.FileInfo.Files.FirstOrDefault(x => x.MD5Hash == file.MD5);
                    if (exiting_file != null)
                    {
                        fileInfo.Duplicate = true;
                    }

                    d_localEntry.FileInfo.Files.Add(fileInfo);
                }
                else
                {
                    d_localEntry.FileInfo = new Data.Common.Metadata.FileCollectionMetaInfo();
                    d_localEntry.FileInfo.Files.Add(fileInfo);
                }

                //Set the sync status
                d_localEntry.Timestamp = DateTime.UtcNow;
                d_localEntry.LocalStatus = LocalSyncStatus.CHANGED;

                var data_store = new LocalDataStore();
                data_store.UpdateMediaEntry(d_localEntry);
            }
            else if (d_tmdbmovie != null)
            {
                //Generic
                var composition = d_tmdbmovie.ToComposition();

                //Cast
                var d_tmdbcast = TmdbClient.Tmdb.GetMovieCast(d_tmdbmovie.id);
                var cast = d_tmdbcast.cast != null ? d_tmdbcast.cast.ToPerson() : new List<Data.Common.Metadata.PersonMetaInfo>();
                cast = d_tmdbcast.crew != null ? cast.Concat(d_tmdbcast.crew.ToPerson()).ToList() : cast;
                composition.Cast = cast;

                //File info
                var fileInfo = FillFileMetaInfo(file.FilePath, file.MD5);

                //Id info
                var idInfo = new Data.Common.Metadata.IDMetaInfo() { TmdbID = d_tmdbmovie.id, ImdbID = d_tmdbmovie.imdb_id };

                //Trailers
                try
                {
                    var trailers = TmdbClient.Tmdb.GetMovieTrailers(d_tmdbmovie.id);
                    if (trailers != null && trailers.youtube != null)
                    {
                        foreach (var tr in trailers.youtube)
                        {
                            idInfo.TrailerInfo.Add(new Data.Common.Metadata.TrailerMetaInfo() { ID = tr.source, Title = tr.name });
                        }
                    }
                }
                catch (Exception) { }

                //Rack entry
                MediaEntry newwentry = new MediaEntry()
                {
                    IDInfo = idInfo,
                    Classification = file.ContentType,
                    LocalStatus = LocalSyncStatus.NEW,
                    Timestamp = DateTime.UtcNow,
                    CompositionInfo = composition,
                    Favorite = file.AddToFavorite,
                    Bookmark = file.AutoBookmark,
                    FileInfo = new Data.Common.Metadata.FileCollectionMetaInfo()
                };
                newwentry.FileInfo.Files.Add(fileInfo);
                var data_store = new LocalDataStore();
                data_store.AddMediaEntry(newwentry);
            }
        }

        // Common

        private MediaRack.Core.Data.Common.Metadata.FileMetaInfo FillFileMetaInfo(string path, string md5)
        {
            var fileInfo = new Data.Common.Metadata.FileMetaInfo();

            //-- Path, File and PC info --
            fileInfo.Root = Path.GetPathRoot(path);
            fileInfo.AbsolutePath = path;
            fileInfo.RootRelativePath = path.Replace(fileInfo.Root, "");
            fileInfo.FileName = Path.GetFileName(path);
            fileInfo.PcName = Environment.MachineName;
            fileInfo.MD5Hash = md5;

            //-- Media info --
            var qualityTags = new List<MediaQuality>();
            MediaInfoDotNet.MediaFile d_mediaInfo = new MediaInfoDotNet.MediaFile(path);
            if (d_mediaInfo.Audio != null && d_mediaInfo.Audio.Count > 0) qualityTags.Add(MediaQuality.Audio);
            if (d_mediaInfo.Video != null && d_mediaInfo.Video.Count > 0) qualityTags.Add(MediaQuality.Video);

            //FileInfo - Video
            if (qualityTags.Contains(MediaQuality.Video))
            {
                //Frame size
                if (path.ToLower().Contains("1080p")) qualityTags.Add(MediaQuality.HD1080P);
                else if (path.ToLower().Contains("720p")) qualityTags.Add(MediaQuality.HD720P);
                else if (path.ToLower().Contains("4k")) qualityTags.Add(MediaQuality.HD4K);
                else if (path.ToLower().Contains("480p")) qualityTags.Add(MediaQuality.HD480P);
                else
                {
                    var frameSize = IdentifyFramesize(d_mediaInfo.Video[0].Width, d_mediaInfo.Video[0].Height);
                    qualityTags.Add(frameSize);
                }

                //Video format
                var vformat = IdentifyVideoFormat(d_mediaInfo.Video[0]);
                qualityTags.AddRange(vformat);

                //Track info
                foreach (var vstream in d_mediaInfo.Video)
                {
                    fileInfo.Tracks.Add(new Data.Common.Metadata.FileTrackMetaInfo() { Title = vstream.Title, TrackID = vstream.ID, TrackType = FileTrackType.Video });
                }
            }

            //FileInfo - Video
            if (qualityTags.Contains(MediaQuality.Audio))
            {
                //Audio format
                var aformat = IdentifyAudioFormat(d_mediaInfo.Audio[0]);
                qualityTags.AddRange(aformat);

                //Track info
                foreach (var vstream in d_mediaInfo.Audio)
                {
                    fileInfo.Tracks.Add(new Data.Common.Metadata.FileTrackMetaInfo() { Title = vstream.Title, TrackID = vstream.ID, TrackType = FileTrackType.Audio });
                }
            }

            //FileInfo - Container
            try
            {
                var container = IdentifyContainer(d_mediaInfo.General.Format, d_mediaInfo.General.FormatInfo, d_mediaInfo);
                if (container.HasValue)
                {
                    qualityTags.Add(container.Value);
                }
                else
                {
                    container = IdentifyContainer(path);
                    if (container.HasValue) qualityTags.Add(container.Value);
                }
            }
            catch (Exception) { }

            fileInfo.QualityTags.AddRange(qualityTags);

            //FileInfo - Rip
            var riptype = IdentifyRip(path);
            if (riptype.HasValue) fileInfo.QualityTags.Add(riptype.Value);

            //FileInfo - Subtitles

            //Embedded
            if (d_mediaInfo.Text != null && d_mediaInfo.Text.Count > 0)
            {
                foreach (var sdata in d_mediaInfo.Text)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(sdata.Format) && sdata.Format.ToLower().Contains("timed"))
                        {
                            var lang = Util.Globalization.LanguageCode.FindCode(sdata.Language);
                            var lstr = lang != null ? lang.EnglishName : defaultCode.EnglishName;
                            fileInfo.Subtitles.Add(new Data.Common.Metadata.SubtitleMetaInfo() { IsEmbedded = true, Language = lstr });
                        }
                    }
                    catch (Exception) { continue; }
                }
            }

            //Folder
            try
            {
                var found_subs = FindSubtitleFiles(path);
                found_subs.ForEach(x => { fileInfo.Subtitles.Add(x); });
            }
            catch (Exception) { }

            //Return value
            return fileInfo;
        }

        private MediaQuality IdentifyFramesize(int width, int height)
        {
            if (height > 1080 && width > 1920) return MediaQuality.HD4K;
            if (height > 720 && width > 1280) return MediaQuality.HD1080P;
            if (height > 480 && width >= 640) return MediaQuality.HD720P;
            return MediaQuality.HD480P;
        }

        private List<MediaQuality> IdentifyVideoFormat(MediaInfoDotNet.Models.VideoStream videoInfo)
        {
            var format = string.IsNullOrWhiteSpace(videoInfo.Format) ? string.Empty : videoInfo.Format.ToLower();
            var longName = string.IsNullOrWhiteSpace(videoInfo.FormatInfo) ? string.Empty : videoInfo.FormatInfo.ToLower();
            var codecId = string.IsNullOrWhiteSpace(videoInfo.CodecId) ? string.Empty : videoInfo.CodecId.ToLower();

            var rvalue = new List<MediaQuality>();

            if (StringUtils.Contains(format, "mpeg-4 visual"))
            {
                //MP4 videos layer
                rvalue.Add(MediaQuality.MP4V);

                if (StringUtils.Contains(videoInfo.CodecId, "dv"))
                {
                    rvalue.Add(MediaQuality.DivX);
                }
                else if (StringUtils.Contains(videoInfo.CodecId, "xvid"))
                {
                    rvalue.Add(MediaQuality.Xvid);
                }
            }
            else if (StringUtils.Contains(format, "mpeg video"))
            {
                if (StringUtils.Contains(videoInfo.FormatVersion, "version 2"))
                {
                    rvalue.Add(MediaQuality.MPEG2);
                }
            }
            else if (StringUtils.Contains(format, "vp") && (StringUtils.Contains(format, "6") || StringUtils.Contains(format, "7") || StringUtils.Contains(format, "8")))
            {
                rvalue.Add(MediaQuality.VP);
            }
            else if (StringUtils.Contains(format, "avc"))
            {
                rvalue.Add(MediaQuality.AVC);
                rvalue.Add(MediaQuality.x264);
            }
            else if (StringUtils.Contains(format, "hevc"))
            {
                rvalue.Add(MediaQuality.HEVC);
                rvalue.Add(MediaQuality.x265);
            }
            return rvalue;
        }

        private List<MediaQuality> IdentifyAudioFormat(MediaInfoDotNet.Models.AudioStream audioStream)
        {
            var format = string.IsNullOrWhiteSpace(audioStream.Format) ? string.Empty : audioStream.Format.ToLower();
            var longName = string.IsNullOrWhiteSpace(audioStream.FormatInfo) ? string.Empty : audioStream.FormatInfo.ToLower();

            var rvalue = new List<MediaQuality>();
            if (format.Contains("mpeg") || longName.Contains("mpeg"))
            {
                rvalue.Add(MediaQuality.MP3);
            }
            if (format.Contains("aac") || longName.Contains("aac"))
            {
                rvalue.Add(MediaQuality.AAC);
            }
            if (format.Contains("wma") || longName.Contains("wma"))
            {
                rvalue.Add(MediaQuality.WMA);
            }
            if (format.Contains("ac-3") || longName.Contains("audio coding 3"))
            {
                rvalue.Add(MediaQuality.AC3);
            }
            if (format.Contains("vorbis") || longName.Contains("vorbis"))
            {
                rvalue.Add(MediaQuality.OGG);
            }
            if (format.Contains("wav") || longName.Contains("wav"))
            {
                rvalue.Add(MediaQuality.WAV);
            }
            if (format.Contains("pcm") || longName.Contains("pcm"))
            {
                rvalue.Add(MediaQuality.WAV);
            }
            return rvalue;
        }

        private MediaQuality? IdentifyContainer(string format, string longName, MediaInfoDotNet.MediaFile mediaInfo)
        {
            format = string.IsNullOrWhiteSpace(format) ? string.Empty : format.ToLower();
            longName = string.IsNullOrWhiteSpace(longName) ? string.Empty : longName.ToLower();

            if (format.Contains("matroska")) return MediaQuality.MKV;
            if (format.Contains("mpeg-4")) return MediaQuality.MP4;
            if (format.Contains("avi")) return MediaQuality.AVI;
            if (format.Contains("webm")) return MediaQuality.WEBM;
            if (format.Contains("flash")) return MediaQuality.FLV;
            if (format.Contains("quicktime")) return MediaQuality.MOV;
            if (format.Contains("windows")) return MediaQuality.WMV;
            return new MediaQuality?();
        }

        private MediaQuality? IdentifyContainer(string path)
        {
            if (path.ToLower().EndsWith("mkv")) return MediaQuality.MKV;
            if (path.ToLower().EndsWith("mp4")) return MediaQuality.MP4;
            if (path.ToLower().EndsWith("wmv")) return MediaQuality.WMV;
            if (path.ToLower().EndsWith("flv")) return MediaQuality.FLV;
            if (path.ToLower().EndsWith("mov")) return MediaQuality.MOV;
            if (path.ToLower().EndsWith("qt")) return MediaQuality.QT;
            if (path.ToLower().EndsWith("webm")) return MediaQuality.WEBM;
            if (path.ToLower().EndsWith("vob")) return MediaQuality.VOB;
            return new MediaQuality?();
        }

        private MediaQuality? IdentifyRip(string path)
        {
            if (StringUtils.Contains(path, "web") && StringUtils.Contains(path, "rip"))
                return MediaQuality.WebRip;
            if (StringUtils.Contains(path, "dvd") && StringUtils.Contains(path, "rip"))
                return MediaQuality.WebRip;
            if (StringUtils.Contains(path, "bluray"))
                return MediaQuality.WebRip;

            return new MediaQuality?();
        }

        private List<Core.Data.Common.Metadata.SubtitleMetaInfo> FindSubtitleFiles(string path)
        {
            var subs = new List<MediaRack.Core.Data.Common.Metadata.SubtitleMetaInfo>();
            var filename = Path.GetFileNameWithoutExtension(path);
            var dir = Path.GetDirectoryName(path);

            //Format for SRT, SMI, SSA, ASS, VTT --> Movie_Name (Release Date).[Language_Code].ext

            //SRT files
            var files_list = Util.IO.FileSystem.ListFiles(dir, SearchOption.TopDirectoryOnly, Util.Configuration.ConfigData.COMPATIBLE_EXTERNAL_SUBFILES);
            foreach (var srtFile in files_list)
            {
                var fileName = Path.GetFileNameWithoutExtension(srtFile);
                var fileNameWithExt = Path.GetFileName(srtFile);

                var splitted = filename.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Length > 1)
                {
                    var langCodeStr = splitted[splitted.Length - 1]; //Last index
                    var lang = Util.Globalization.LanguageCode.FindCode(langCodeStr);
                    var dtitle = lang != null ? lang.EnglishName : defaultCode.EnglishName;
                    subs.Add(new Data.Common.Metadata.SubtitleMetaInfo() { Filename = fileNameWithExt, Language = dtitle, IsEmbedded = false });
                }
                else
                {
                    subs.Add(new Data.Common.Metadata.SubtitleMetaInfo() { Filename = fileNameWithExt, Language = defaultCode.EnglishName, IsEmbedded = false });
                }
            }

            //VobSub
            files_list = Util.IO.FileSystem.ListFiles(dir, SearchOption.TopDirectoryOnly, ".idx");
            foreach (var srtFile in files_list)
            {
                var fileName = Path.GetFileNameWithoutExtension(srtFile);
                var fileNameWithExt = Path.GetFileName(srtFile);
                var sub_file = Path.Combine(dir, filename, ".sub");

                if (File.Exists(sub_file))
                {
                    var splitted = filename.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitted.Length > 1)
                    {
                        var langCodeStr = splitted[splitted.Length - 1]; //Last index
                        var lang = Util.Globalization.LanguageCode.FindCode(langCodeStr);
                        var dtitle = lang != null ? lang.EnglishName : defaultCode.EnglishName;
                        subs.Add(new Data.Common.Metadata.SubtitleMetaInfo() { Filename = fileNameWithExt, Language = dtitle, IsEmbedded = false });
                    }
                    else
                    {
                        subs.Add(new Data.Common.Metadata.SubtitleMetaInfo() { Filename = fileNameWithExt, Language = defaultCode.EnglishName, IsEmbedded = false });
                    }
                }
            }

            return subs;
        }


        public void Dispose()
        {
            if (this.queueCheck != null)
            {
                try
                {
                    queueCheck.Stop();
                    queueCheck.Dispose();
                }
                catch (Exception) { }
            }
        }
    }
}
