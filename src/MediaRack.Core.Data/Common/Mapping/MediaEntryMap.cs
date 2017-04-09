using FluentNHibernate.Mapping;
using MediaRack.Core.Data.Common.Metadata;
using NHibernate.JsonColumn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Mapping
{
    public class MediaEntryMap : ClassMap<MediaEntry>
    {
        public MediaEntryMap()
        {
            Table("Rack");
            Id(x => x.MediaRackID).GeneratedBy.Identity();
            Map(x => x.Classification).CustomType<MediaClassification>();
            Map(x => x.LocalStatus, "SyncStatus").CustomType<LocalSyncStatus>().Default("NEW");
            Map(x => x.Watched);
            Map(x => x.WatchedOn);
            Map(x => x.Timestamp);
            Map(x => x.Grade);
            Map(x => x.Comment);
            Map(x => x.IDInfo).CustomType<JsonMappableType<IDMetaInfo>>();
            Map(x => x.CompositionInfo).CustomType<JsonMappableType<CompositionMetaInfo>>();
            Map(x => x.FileInfo).CustomType<JsonMappableType<FileCollectionMetaInfo>>();
        }
    }
}
