using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    /// <summary>
    /// Interface to implement for a remote synced data object
    /// </summary>
    public interface ISyncedDTO
    {
        string GetHash();

        LocalSyncStatus LocalStatus { get; set; }

        string Hash { get; set; }
    }
}
