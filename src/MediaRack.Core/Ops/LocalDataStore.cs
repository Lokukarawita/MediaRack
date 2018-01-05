using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaRack.Core.Data.Local.DAO;
using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;

namespace MediaRack.Core.Ops
{
    public class LocalDataStore
    {
        public MediaEntry GetByTmdbId(int id)
        {
            var dao = new MediaEntryDAO();

            using (var session = dao.GetSession())
            {
                var qry = dao.GetQuery(session);
                var item = qry.FirstOrDefault(x => x.IDInfo != null && x.IDInfo.TmdbID == id);
                return item;
            }
        }
        public MediaEntry GetByMediaRackId(int id)
        {
            var dao = new MediaEntryDAO();

            using (var session = dao.GetSession())
            {
                var qry = dao.GetQuery(session);
                var item = qry.FirstOrDefault(x => x.MediaRackID == id);
                return item;
            }
        }


        public MediaEntry AddMediaEntry(MediaEntry entry)
        {
            var dao = new MediaEntryDAO();
            using (var session = dao.GetSession())
            {
                return dao.Add(entry);
            }
        }
        
        public MediaEntry UpdateMediaEntry(MediaEntry entry)
        {
            var dao = new MediaEntryDAO();
            using (var session = dao.GetSession())
            {
                return dao.Update(entry);
            }
        }

        public void UpdateSyncInfo(SyncMetaInfo syncinf)
        {
            var current_userInfo = UserManagement.GetCurrentUser();
            var idx = current_userInfo.Settings.SyncInfo.FindIndex(x => x.PCName == Environment.MachineName);
            if (idx > -1)
                current_userInfo.Settings.SyncInfo[idx] = syncinf;
            else
                current_userInfo.Settings.SyncInfo.Add(syncinf);

            var dao = new UserInfoDAO();
            dao.Update(current_userInfo);
        }

    }
}
