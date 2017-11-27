using MediaRack.Core.Data.Common.Metadata;
using MediaRack.Core.Util.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MediaRack.Core.Ops
{
    public class DirectoryWatch : IDisposable
    {
        private log4net.ILog log;
        private bool isProcessing;
        private Timer timer;

        public event EventHandler DirectoryChecked;

        public DirectoryWatch(WatchDirMetaInfo dir)
        {
            DirectoryInfo = dir;
            Filters = MediaRack.Core.Util.Configuration.ConfigData.COMPATIBLE_MEDIA_TYPES;
            Init();
        }
        public DirectoryWatch(WatchDirMetaInfo dir, string[] mediaTypeFilters)
        {
            DirectoryInfo = dir;
            Filters = mediaTypeFilters;
            Init();
        }

        private void Init()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            timer = new Timer();
            timer.Elapsed += timer_Elapsed;

            var seconds = ConfigKeys.KEY_DIR_SCANINTERVAL.GetConfigValue<double>(60);
            timer.Interval = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isProcessing)
                return;
            try
            {
                isProcessing = true;
                var enumbrl = Filters.SelectMany(x => System.IO.Directory.EnumerateFiles(DirectoryInfo.Directory, "*" + x, System.IO.SearchOption.AllDirectories));
                Parallel.ForEach<string>(enumbrl,
                    new ParallelOptions() { MaxDegreeOfParallelism = 5 },
                    x =>
                    {
                        try
                        {
                            Core.Data.Local.LocalFileQueue.Instance.AddFile(x);
                            log.DebugFormat("DIR_WATCH: File added {0}", x);
                        }
                        catch (Exception ex)
                        {
                            log.DebugFormat("DIR_WATCH: File added error {0}", ex.Message);
                        }

                    });

                if (DirectoryChecked != null)
                    DirectoryChecked(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                log.DebugFormat("DIR_WATCH: File added error {0}", ex.Message);
            }


            isProcessing = false;
        }

        public WatchDirMetaInfo DirectoryInfo { get; private set; }
        public string[] Filters { get; private set; }

        public void Dispose()
        {
            if (timer != null)
            {
                try
                {
                    timer.Stop();
                    timer.Dispose();
                }
                catch (Exception) { }
            }
        }
    }
}
