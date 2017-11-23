using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MediaRack.Core.Scanning
{
    public class DirectoryWatch : IDisposable
    {
        private bool isProcessing;
        private Timer timer;

        public DirectoryWatch(string dir)
        {
            Directory = dir;
        }

        private void Init()
        {
            timer = new Timer();
            timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isProcessing)
                return;

            isProcessing = true;
            var enumbrl = Filters.SelectMany(x => System.IO.Directory.EnumerateFiles(Directory, x)).ToList();
           //
            isProcessing = false;
        }

        public string Directory { get; private set; }
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
