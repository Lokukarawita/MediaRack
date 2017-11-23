using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Local.DAO
{
    public class FileQueueDAO : BaseDAO<LocalFileQueueItem, int>
    {
        public bool ExistsByHash(string hash)
        {
            using (var ses = GetSession())
            {
                var query = GetQuery(ses);
                return query.Count(x => x.MD5.Equals(hash)) > 0;
            }
        }
        public bool ExistsByPath(string path)
        {
            using (var ses = GetSession())
            {
                var query = GetQuery(ses);
                return query.Count(x => x.FilePath.ToLower().Equals(path.ToLower())) > 0;
            }
        }
    }
}
