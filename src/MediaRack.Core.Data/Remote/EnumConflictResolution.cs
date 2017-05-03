using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public enum ConflictResolution
    {
        /// <summary>
        /// Keep the remote file
        /// </summary>
        KeepRemote,
        /// <summary>
        /// Keep the local file
        /// </summary>
        KeepLocal,
        /// <summary>
        /// Throw error
        /// </summary>
        Throw,
        /// <summary>
        /// Ignore 
        /// </summary>
        IgonreAndContinue
    }
}
