using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    /// <summary>
    /// Interface to implement on a sychronizable object which can be used to synchonized with
    /// a remote data store
    /// </summary>
    public interface ISynchronizable
    {

        LocalSyncStatus LocalStatus { get; set; }

        DateTime Timestamp { get; set; }
    }
}
