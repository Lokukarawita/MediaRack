using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public class SyncError
    {
        public ISynchronizable Element { get; set; }
        
        public string Error { get; set; }
    }
}
