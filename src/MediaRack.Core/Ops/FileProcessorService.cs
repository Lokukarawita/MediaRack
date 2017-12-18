using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Configuration;
using MediaRack.Core.Util.Patterns;
using MediaRack.Core.Api.TMDDb;
using MediaRack.Core.Api.Mapping;
using System.IO;
using MediaRack.Core.Data.Common;

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
                        ProcessMovieEntry(file);
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
                var ptrn_data = file.FilePath.ExtractPathData(file.FolderPattern);

                if (ptrn_data.Count > 0)
                {
                    if (ptrn_data.Keys.Contains("|moviename|"))
                        d_moviename = ptrn_data["|moviename|"];
                    if (ptrn_data.Keys.Contains("|year|"))
                    {
                        var year_str = ptrn_data["|year|"];
                        if (!int.TryParse(year_str, out d_movieyear)) { d_movieyear = default(int); }
                    }
                    else
                    {
                        d_movieyear = default(int);
                    }
                }
            }
            else
            {
                //File name extraction
                d_movieyear = default(int);
                d_moviename = file.FilePath.ExtractMovieName();
            }

            //Error check
            if (string.IsNullOrWhiteSpace(d_moviename))
            {
                Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, Data.Common.LocalFileProcessState.Error);
                processing = false;
                return;
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
                Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, Data.Common.LocalFileProcessState.Error);
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
                Data.Local.LocalFileQueue.Instance.ReleaseFile(file.FileId, Data.Common.LocalFileProcessState.Error);
            }


            if (d_localEntry != null)
            {

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

                var fileInfo = new Data.Common.Metadata.FileMetaInfo();
            }

        }

        private void FillFileMetaInfo(string path, string md5)
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

            //Media info - Video
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
                var vformat = IdentifyVideoFormat(d_mediaInfo.Video[0].Format, d_mediaInfo.Video[0].FormatInfo, d_mediaInfo.Video[0].CodecId);
                qualityTags.AddRange(vformat);

                //Track info
                foreach (var vstream in d_mediaInfo.Video)
                {
                    fileInfo.Tracks.Add(new Data.Common.Metadata.FileTrackMetaInfo() { Title = vstream.Title, TrackID = vstream.ID, TrackType = FileTrackType.Video });
                }
            }

            //Media info - Video
            if (qualityTags.Contains(MediaQuality.Audio))
            {
                //Audio format
                var aformat = IdentifyVideoFormat(d_mediaInfo.Audio[0].Format, d_mediaInfo.Audio[0].FormatInfo, d_mediaInfo.Audio[0].CodecId);
                qualityTags.AddRange(aformat);

                //Track info
                foreach (var vstream in d_mediaInfo.Audio)
                {
                    fileInfo.Tracks.Add(new Data.Common.Metadata.FileTrackMetaInfo() { Title = vstream.Title, TrackID = vstream.ID, TrackType = FileTrackType.Audio });
                }
            }

            //Media info - Subtitles
            try
            {
                //Embedded
                if (d_mediaInfo.Text != null && d_mediaInfo.Text.Count > 0)
                {
                    foreach (var sdata in d_mediaInfo.Text)
                    {
                        if (!string.IsNullOrWhiteSpace(sdata.Format) && sdata.Format.ToLower().Contains("timed"))
                        {
                            var lang = Util.Configuration.LanguageCode.FindCode(sdata.Language);
                            var lstr = lang != null ? lang.EnglishName : defaultCode.EnglishName;
                            fileInfo.Subtitles.Add(new Data.Common.Metadata.SubtitleMetaInfo() { IsEmbedded = true, Language = lstr });
                        }
                    }
                }

                //Folder

            }
            catch (Exception) { }

            //Media info - Container
            try
            {
                var container = IdentifyContainer(d_mediaInfo.General.Format, d_mediaInfo.General.FormatInfo, d_mediaInfo.General.codecId);
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
        }


        private MediaQuality IdentifyFramesize(int width, int height)
        {
            if (height > 1080 && width > 1920) return MediaQuality.HD4K;
            if (height > 720 && width > 1280) return MediaQuality.HD1080P;
            if (height > 480 && width >= 640) return MediaQuality.HD720P;
            return MediaQuality.HD480P;
        }
        private List<MediaQuality> IdentifyVideoFormat(string format, string longName, string mediaInfoid = null)
        {
            format = string.IsNullOrWhiteSpace(format) ? string.Empty : format;
            longName = string.IsNullOrWhiteSpace(longName) ? string.Empty : longName;
            mediaInfoid = string.IsNullOrWhiteSpace(mediaInfoid) ? string.Empty : mediaInfoid;

            var rvalue = new List<MediaQuality>();

            if (format.ToLower() == "avc" || format.ToLower().Contains("264") || longName.ToLower().Contains("264"))
            {
                rvalue.Add(MediaQuality.AVC);
                rvalue.Add(MediaQuality.x264);
            }
            if (format.ToLower() == "hevc" || format.ToLower().Contains("265") || longName.ToLower().Contains("265"))
            {
                rvalue.Add(MediaQuality.HEVC);
                rvalue.Add(MediaQuality.x265);
            }
            if (format.ToLower().Contains("WMV") || longName.ToLower().Contains("WMV"))
            {
                rvalue.Add(MediaQuality.WMV);
            }
            if (format.ToLower().Contains("xvid") || longName.ToLower().Contains("xvid"))
            {
                rvalue.Add(MediaQuality.Xvid);
            }
            if (format.ToLower().Contains("divx") || longName.ToLower().Contains("divx"))
            {
                rvalue.Add(MediaQuality.DivX);
            }
            if (format.ToLower().Contains("vp") || longName.ToLower().Contains("vp"))
            {
                rvalue.Add(MediaQuality.VP);
            }

            return rvalue;
        }
        private List<MediaQuality> IdentifyAudioFormat(string format, string longName, string mediaInfoid = null)
        {
            format = string.IsNullOrWhiteSpace(format) ? string.Empty : format;
            longName = string.IsNullOrWhiteSpace(longName) ? string.Empty : longName;
            mediaInfoid = string.IsNullOrWhiteSpace(mediaInfoid) ? string.Empty : mediaInfoid;

            var rvalue = new List<MediaQuality>();
            if (format.ToLower().Contains("mpeg") || longName.ToLower().Contains("mpeg"))
            {
                rvalue.Add(MediaQuality.MP3);
            }
            if (format.ToLower().Contains("aac") || longName.ToLower().Contains("aac"))
            {
                rvalue.Add(MediaQuality.AAC);
            }
            if (format.ToLower().Contains("wma") || longName.ToLower().Contains("wma"))
            {
                rvalue.Add(MediaQuality.WMA);
            }
            if (format.ToLower().Contains("ac3") || longName.ToLower().Contains("ac3"))
            {
                rvalue.Add(MediaQuality.WMA);
            }
            if (format.ToLower().Contains("ogg") || longName.ToLower().Contains("ogg"))
            {
                rvalue.Add(MediaQuality.WMA);
            }
            if (format.ToLower().Contains("wav") || longName.ToLower().Contains("wav"))
            {
                rvalue.Add(MediaQuality.WAV);
            }
            if (format.ToLower().Contains("pcm") || longName.ToLower().Contains("pcm"))
            {
                rvalue.Add(MediaQuality.WAV);
            }
            return rvalue;
        }
        private MediaQuality? IdentifyContainer(string format, string longName, string mediaInfoId = null)
        {
            format = string.IsNullOrWhiteSpace(format) ? string.Empty : format;
            longName = string.IsNullOrWhiteSpace(longName) ? string.Empty : longName;
            mediaInfoId = string.IsNullOrWhiteSpace(mediaInfoId) ? string.Empty : mediaInfoId;

            if (format.ToLower().Contains("matroska")) return MediaQuality.MKV;
            if (format.ToLower().Contains("mpeg-4")) return MediaQuality.MP4;
            if (format.ToLower().Contains("avi")) return MediaQuality.AVI;
            if (format.ToLower().Contains("webm")) return MediaQuality.WEBM;
            if (format.ToLower().Contains("flash")) return MediaQuality.FLV;
            if (format.ToLower().Contains("quicktime")) return MediaQuality.MOV;
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
            return new MediaQuality?();
        }

        private Core.Data.Common.Metadata.SubtitleMetaInfo[] FindSubtitleFiles(string path)
        {
            var subs = new List<MediaRack.Core.Data.Common.Metadata.SubtitleMetaInfo>();
            var filename = Path.GetFileNameWithoutExtension(path);
            var dir = Path.GetDirectoryName(path);

            //SRT files
            foreach (var srtFile in Directory.EnumerateFiles(dir, "*.srt", SearchOption.TopDirectoryOnly))
            {
                var regex = new System.Text.RegularExpressions.Regex(@"[^\.\s_-]+.+(?=\.srt)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (regex.IsMatch(srtFile))
                {
                    var fileName = Path.GetFileNameWithoutExtension(srtFile);
                    var matched = regex.Match(srtFile).Value;
                    matched = matched.Replace(fileName, "").Trim();
                    if (!string.IsNullOrWhiteSpace(matched))
                    {
                        var lang = Util.Configuration.LanguageCode.FindCode(matched);
                        var dtitle = lang != null ? lang.EnglishName : defaultCode.EnglishName;
                        subs.Add(new Data.Common.Metadata.SubtitleMetaInfo() { Filename = Path.GetFileName(srtFile), Language = dtitle, IsEmbedded = false });
                    }
                }
            }

            //VobSub

            return subs.ToArray();
        }



        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
