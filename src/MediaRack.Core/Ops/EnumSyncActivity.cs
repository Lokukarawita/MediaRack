using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Ops
{
    public enum SyncActivity
    {
        Idle = 0,
        Started,
        Paused,
        LostConnection,
        Error
    }
}
