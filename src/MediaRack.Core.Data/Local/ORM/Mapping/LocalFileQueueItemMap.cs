using FluentNHibernate.Mapping;
using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Local.ORM.Mapping
{
    public class LocalFileQueueItemMap : ClassMap<LocalFileQueueItem>
    {
        public LocalFileQueueItemMap()
        {
            Table("FileQueue");
            Id(x => x.FileId).GeneratedBy.Identity();
            Map(x => x.FilePath);
            Map(x => x.FileSize);
            Map(x => x.MD5);       
            Map(x => x.Added);
            Map(x => x.ProcessStatus).CustomType<Data.Common.LocalFileProcessState>().Column("Status");
            Map(x => x.TryCount).Default("0");
            Map(x => x.FolderPattern);
        }
    }
}
