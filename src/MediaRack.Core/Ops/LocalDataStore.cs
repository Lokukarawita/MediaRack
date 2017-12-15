using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaRack.Core.Data.Local.DAO;
using MediaRack.Core.Data.Common;

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
    }
}
