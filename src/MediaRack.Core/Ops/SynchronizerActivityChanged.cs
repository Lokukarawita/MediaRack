using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Ops
{
    public class SynchronizerActivityChangedEventArgs : EventArgs
    {
        public SyncActivity CurrentActivity { get; set; }
        public SyncActivity NextActivity { get; set; }
    }
}
