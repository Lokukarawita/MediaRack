using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.DAO
{
    public class MediaEntryDAO : BaseDAO<MediaEntry, int>
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
    }
}
