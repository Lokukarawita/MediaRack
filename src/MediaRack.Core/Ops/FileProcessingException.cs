using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Ops
{
    [Serializable]
    public class FileProcessingException : Exception
    {
        public FileProcessingException() { }
        public FileProcessingException(string message) : base(message) { }
        public FileProcessingException(string message, Exception inner) : base(message, inner) { }
        protected FileProcessingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
