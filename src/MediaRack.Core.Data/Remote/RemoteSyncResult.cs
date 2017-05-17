using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public class RemoteSyncResult
    {
        public RemoteSyncResult()
        {
            Synced = new List<ISynchronizable>();
            Errors = new List<ISynchronizable>();
        }

        public List<ISynchronizable> Synced { get; set; }
        public List<ISynchronizable> Errors { get; set; }
    }
}
