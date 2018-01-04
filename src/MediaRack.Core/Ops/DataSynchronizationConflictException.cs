using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Ops
{
    [Serializable]
    public class DataSynchronizationConflictException : Exception
    {
        public DataSynchronizationConflictException() { }
        public DataSynchronizationConflictException(string message) : base(message) { }
        public DataSynchronizationConflictException(string message, Exception inner) : base(message, inner) { }
        protected DataSynchronizationConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public MediaRack.Core.Data.Remote.ConflictResolution CurrentResolution { get; set; }
    }
}
