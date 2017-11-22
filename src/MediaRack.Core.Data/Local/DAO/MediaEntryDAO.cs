using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Local.DAO
{
    public class MediaEntryDAO : BaseDAO<MediaEntry, int>, ISyncedControlDTO<MediaEntry>
    {
        public override void Add(MediaEntry item)
        {
            item.LocalStatus = LocalSyncStatus.NEW;
            item.Timestamp = DateTime.UtcNow;
            base.Add(item);
        }

        public override void Update(MediaEntry item)
        {
            item.LocalStatus = LocalSyncStatus.CHANGED;
            item.Timestamp = DateTime.UtcNow;
            base.Update(item);
        }

        public void MarkForDelete(MediaEntry entity)
        {
            entity.LocalStatus = LocalSyncStatus.DELETED;
            base.Update(entity);
        }
    }
}
