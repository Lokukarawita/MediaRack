using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    [Serializable]
    public class RemoteStorageException : Exception
    {
        public RemoteStorageException() { }
        public RemoteStorageException(string message) : base(message) { }
        public RemoteStorageException(string message, Exception inner) : base(message, inner) { }
        protected RemoteStorageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
