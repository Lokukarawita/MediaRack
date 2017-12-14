using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Configuration;
using MediaRack.Core.Util.Patterns;
using MediaRack.Core.Api.TMDDb;

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
                    //Data
                    string d_moviename = string.Empty;
                    int d_movieyear = default(int);
                    WatTmdb.V3.MovieResult d_serchResult = null;
                    WatTmdb.V3.TmdbMovie d_movie = null;

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


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
