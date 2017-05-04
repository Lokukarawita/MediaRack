using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    [Serializable]
    public class RemoteStorageAuthenticationException : Exception
    {
        public RemoteStorageAuthenticationException() { }
        public RemoteStorageAuthenticationException(string message) : base(message) { }
        public RemoteStorageAuthenticationException(string message, Exception inner) : base(message, inner) { }
        protected RemoteStorageAuthenticationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
