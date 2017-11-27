using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Configuration;

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
                file= Data.Local.LocalFileQueue.Instance.TakeFile();
                if (file != null)
                {
                    //Do process
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
