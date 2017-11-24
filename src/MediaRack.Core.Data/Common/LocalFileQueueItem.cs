﻿using System;
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
    }
}