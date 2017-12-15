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

namespace MediaRack.Core.Ops
{
    public class FileProcessorService : IDisposable
    {
        private log4net.ILog log;
        private Timer queueCheck;
        private bool processing;

        public FileProcessorService()
        {
            queueCheck = new Timer();
            queueCheck.Elapsed += queueCheck_Elapsed;

            var interval = ConfigKeys.KEY_FILPROC_QUEUECHECKINTERVAL.GetConfigValue<int>(60);
            queueCheck.Interval = TimeSpan.FromSeconds(interval).TotalMilliseconds;

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

            //Path, File and PC info
            fileInfo.Root = Path.GetPathRoot(path);
            fileInfo.AbsolutePath = path;
            fileInfo.RootRelativePath = path.Replace(fileInfo.Root, "");
            fileInfo.FileName = Path.GetFileName(path);
            fileInfo.PcName = Environment.MachineName;

            //Media info
            MediaInfoDotNet.MediaFile d_mediaInfo = new MediaInfoDotNet.MediaFile(path);
            

        }



        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
