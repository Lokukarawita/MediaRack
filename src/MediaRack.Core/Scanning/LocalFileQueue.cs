using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Local.DAO;
using MediaRack.Core.Util.Hash;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Scanning
{
    public class LocalFileQueue
    {
        #region Singleton
        private static LocalFileQueue _queue;
        public static LocalFileQueue Instance
        {
            get
            {
                if (_queue == null)
                    _queue = new LocalFileQueue();

                return _queue;
            }

        }
        #endregion

        private object lockDao;
        private FileQueueDAO dao;

        public LocalFileQueue()
        {
            dao = new FileQueueDAO();
        }

        public void Enqueue(string path)
        {
            var fileInfo = new System.IO.FileInfo(path);
            var hash = HashUtil.HashFile(path);
            if (!dao.ExistsByHash(hash))
            {
                var item = new LocalFileQueueItem()
                {
                    FilePath = path,
                    MD5 = hash,
                    Processed = false,
                    Added = DateTime.UtcNow,
                    FileSize = fileInfo.Length
                };
                lock (lockDao)
                {
                    dao.Add(item);
                }
            }
            //else
            //{
            //    lock (lockDao)
            //    {
            //        var elem = dao.Get(x => x.Processed == false).FirstOrDefault();
            //    }
            //}
        }

        public LocalFileQueueItem Dequeue()
        {
            lock (lockDao)
            {
                var elem = dao.Get(x => x.Processed == false).FirstOrDefault();
                if (elem != null)
                {
                    elem.Processed = true;
                    dao.Update(elem);
                }
                return elem;
            }
        }
    }
}
