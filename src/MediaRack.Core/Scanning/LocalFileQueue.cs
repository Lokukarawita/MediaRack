using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Scanning
{
    public class LocalFileQueue : ConcurrentQueue<LocalFile>
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
    }
}
