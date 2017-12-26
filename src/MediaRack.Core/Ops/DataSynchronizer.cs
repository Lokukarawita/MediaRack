using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Configuration;

namespace MediaRack.Core.Ops
{
    public class DataSynchronizer : IDisposable
    {
        private Timer changeCheckTimer;

        private event EventHandler SyncStatusChanged;
        private event EventHandler SyncDirectionChanged;

        public DataSynchronizer()
        {
            changeCheckTimer = new Timer();

            var interval = ConfigKeys.KEY_SYNCZ_INTERVAL.GetConfigValue<long>(120);
            changeCheckTimer.Interval = TimeSpan.FromSeconds(interval).TotalMilliseconds;
            
            changeCheckTimer.Elapsed += changeCheckTimer_Elapsed;
        }

        private void changeCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Check connectivity
            //Check active status
            //Raise event for direction change
            //Start up sync
            //Raise event for direction change
            //Start done sync
            //Raise event for status change
        }


        public SyncActivity CurrentStatus { get; private set; }
        public SyncDirection CurrentSyncDirection { get; private set; }

        public void Dispose()
        {
            
        }
    }
}
