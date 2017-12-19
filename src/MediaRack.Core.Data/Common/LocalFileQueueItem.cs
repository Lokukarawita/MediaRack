using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common
{
    public class LocalFileQueueItem
    {
        public virtual int FileId { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string MD5 { get; set; }
        public virtual long FileSize { get; set; }
        public virtual DateTime Added { get; set; }
        public virtual LocalFileProcessState ProcessStatus { get; set; }
        public virtual int TryCount { get; set; }
        public virtual string FolderPattern { get; set; }
        public virtual MediaClassification ContentType { get; set; }
        public virtual bool AddToFavorite { get; set; }
        public virtual string AutoBookmark { get; set; }
    }
}
