using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common
{
    public class LocalFileQueueItem
    {
        public int FileId { get; set; }
        public string FilePath { get; set; }
        public string MD5 { get; set; }
        public long FileSize { get; set; }
        public bool Processed { get; set; }
        public DateTime Added { get; set; }
    }
}
