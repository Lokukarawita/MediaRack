using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MediaRack.Core.Util.Net;
using MediaRack.Core.Util.Configuration;
using MediaRack.Core.Data.Remote;
using MediaRack.Core.Data.Local;
using MediaRack.Core.Data.Common;

namespace MediaRack.Core.Ops
{
    public class DataSynchronizer : IDisposable
    {
        private Timer changeCheckTimer;

        private string remote_server;
        private long rsync_interval;
        private bool connection_check_started;
        private long connection_check_interval;

        public event EventHandler<SynchronizerActivityChangedEventArgs> SyncActivityChanged;
        public event EventHandler SyncDirectionChanged;

        public DataSynchronizer()
        {
            //Settings
            rsync_interval = ConfigKeys.KEY_SYNCZ_INTERVAL.GetConfigValue<long>(120);
            remote_server = ConfigKeys.KEY_RDATA_SERVER.GetConfigValue<string>("localhost");
            connection_check_interval = ConfigKeys.KEY_CONN_CHKINTERVAL.GetConfigValue<long>(60);

            //Current activity
            CurrentStatus = SyncActivity.Idle;

            //Last run
            LastSuccessRun = DateTime.MinValue;

            //Start timer for sync
            StartTimer(rsync_interval);
        }


        private void ChangeActivity(SyncActivity activity)
        {
            var evt = new SynchronizerActivityChangedEventArgs()
            {
                CurrentActivity = CurrentStatus,
                NextActivity = activity
            };
            CurrentStatus = SyncActivity.Idle;
            if (SyncActivityChanged != null)
                SyncActivityChanged(this, evt);
        }

        private void ChangeDirection(SyncDirection direction)
        {
            CurrentSyncDirection = direction;
            if (SyncDirectionChanged != null)
                SyncDirectionChanged(this, EventArgs.Empty);
        }

        private void StartTimer(long interval)
        {
            if (changeCheckTimer != null)
            {
                if (changeCheckTimer.Enabled)
                {
                    changeCheckTimer.Stop();
                }

                changeCheckTimer.Dispose();
                changeCheckTimer = null;
            }

            changeCheckTimer = new Timer();
            changeCheckTimer.Interval = TimeSpan.FromSeconds(interval).TotalMilliseconds;
            changeCheckTimer.Elapsed += changeCheckTimer_Elapsed;
            changeCheckTimer.Start();
        }

        private bool IsConnected()
        {
            return NetworkHelper.Ping(remote_server);
        }

        private bool BeginCheckConnectivity()
        {
            var remoteok = IsConnected();
            if (remoteok)
            {
                ChangeActivity(CurrentStatus = SyncActivity.Idle);
                connection_check_started = false;
                StartTimer(rsync_interval);
                return true;
            }
            else
            {
                if (!connection_check_started)
                {
                    ChangeActivity(SyncActivity.LostConnection);
                    connection_check_started = true;
                    StartTimer(connection_check_interval);
                }

                return false;
            }
        }

        private void BeginSyncProcess()
        {

            //Check connectivity
            if (IsConnected())
            {
                //Check active status
                ChangeActivity(SyncActivity.Started);
                //Raise event for direction change
                ChangeDirection(SyncDirection.Down);
                //Start down sync
                SyncDownProcess();
                //Raise event for direction change
                ChangeDirection(SyncDirection.Up);
                //Start up sync
                SyncUpProcess();
                //Set last run
                LastSuccessRun = DateTime.Now;
                //Raise event for status change
                ChangeActivity(SyncActivity.Idle);
            }
            else
            {
                //
                BeginCheckConnectivity();
            }
        }

        private void SyncDownProcess()
        {
            MYSQLRemoteStorage rstorage = new MYSQLRemoteStorage();
            //rstorage.Connect();
            //rstorage.GetRemote()
        }

        private void SyncUpProcess()
        {

        }

        private void changeCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (CurrentStatus)
            {
                case SyncActivity.Started:                  //Already active
                case SyncActivity.Paused:                   //Paused on user request
                    return;
                case SyncActivity.LostConnection:           //Connection paused due to loss of connection     
                    BeginCheckConnectivity();
                    break;
                case SyncActivity.Idle:
                    BeginSyncProcess();
                    break;
                case SyncActivity.Error: break;
                default: break;
            }



        }


        public void Pause()
        {
            changeCheckTimer.Stop();
            ChangeActivity(SyncActivity.Paused);
        }

        public void Resume()
        {
            ChangeActivity(SyncActivity.Idle);
            StartTimer(rsync_interval);
        }

        public void Dispose()
        {
            if (changeCheckTimer != null)
                changeCheckTimer.Dispose();
        }


        public SyncActivity CurrentStatus { get; private set; }

        public SyncDirection CurrentSyncDirection { get; private set; }

        public DateTime LastSuccessRun { get; private set; }
    }
}
