using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Local.DAO;
using MediaRack.Core.Util.Hash;
using MediaRack.Core.Util.Configuration;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Local
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
            lockDao = new object();
            dao = new FileQueueDAO();
        }

        public void AddFile(string path, string folderStructureSeq = null, MediaClassification contentType =  MediaClassification.Movie)
        {
            var fileInfo = new System.IO.FileInfo(path);
            var hash = HashUtil.HashFile(path);
            if (!dao.ExistsByHash(hash))
            {
                var item = new LocalFileQueueItem()
                {
                    FilePath = path,
                    MD5 = hash,
                    ProcessStatus = LocalFileProcessState.New,
                    Added = DateTime.UtcNow,
                    FileSize = fileInfo.Length,
                    FolderPattern = folderStructureSeq,
                    ContentType = contentType
                };
                lock (lockDao)
                {
                    dao.Add(item);
                }
            }
        }

        public LocalFileQueueItem TakeFile()
        {
            lock (lockDao)
            {
                var elem = dao.Get(x => x.ProcessStatus == LocalFileProcessState.New).FirstOrDefault();
                if (elem != null)
                {
                    elem.ProcessStatus = LocalFileProcessState.Owned;
                    dao.Update(elem);
                }
                return elem;
            }
        }

        public void ReleaseFile(int id, bool error)
        {
            lock (lockDao)
            {
                var tryCount = ConfigKeys.KEY_LOCALFILE_MAXTRYCOUNT.GetConfigValue<int>(5);
                var elem = dao.Get(x => x.FileId == id).FirstOrDefault();
                if (elem != null)
                {
                    if (error)
                    {
                        if (elem.TryCount >= tryCount)
                            elem.ProcessStatus = LocalFileProcessState.Error;
                        else
                        {
                            elem.TryCount++;
                            elem.ProcessStatus = LocalFileProcessState.New;
                        }
                        dao.Update(elem);
                    }
                    else
                    {
                        elem.ProcessStatus = LocalFileProcessState.Processed;
                        dao.Update(elem);
                    }
                }
            }
        }

        public void ReleaseFile(int id, LocalFileProcessState state)
        {
            lock (lockDao)
            {
                var tryCount = ConfigKeys.KEY_LOCALFILE_MAXTRYCOUNT.GetConfigValue<int>(5);
                var elem = dao.Get(x => x.FileId == id).FirstOrDefault();
                if (elem != null)
                {
                    elem.ProcessStatus = state;
                    elem.TryCount = tryCount;

                    dao.Update(elem);
                }
            }
        }

        public void DiscardFile(int id)
        {
            lock (lockDao)
            {
                var tryCount = ConfigKeys.KEY_LOCALFILE_MAXTRYCOUNT.GetConfigValue<int>(5);
                var elem = dao.Get(x => x.FileId == id).FirstOrDefault();
                if (elem != null)
                {
                    elem.ProcessStatus = LocalFileProcessState.Discarded;
                    dao.Update(elem);
                }
            }
        }
    }
}
