using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Remote
{
    public interface IRemoteStorage
    {
        void Connect();

        void Disconnect();

        void UpdateRemote(List<ISyncedDTO> localData);

        List<ISyncedDTO> GetRemote();

        bool IsConnected { get; }
    }
}
