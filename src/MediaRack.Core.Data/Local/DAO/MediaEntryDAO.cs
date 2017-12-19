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
        public override MediaEntry Add(MediaEntry item)
        {
            item.LocalStatus = LocalSyncStatus.NEW;
            item.Timestamp = DateTime.UtcNow;
            return base.Add(item);
        }

        public override MediaEntry Update(MediaEntry item)
        {
            item.LocalStatus = LocalSyncStatus.CHANGED;
            item.Timestamp = DateTime.UtcNow;
            return base.Update(item);
        }

        public void MarkForDelete(MediaEntry entity)
        {
            entity.LocalStatus = LocalSyncStatus.DELETED;
            base.Update(entity);
        }
    }
}
