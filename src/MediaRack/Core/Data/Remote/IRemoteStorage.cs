using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Remote
{
    public interface IRemoteStorage
    {
        void Open();
        void Close();
        void UpdateRemote(List<ISyncedDTO> localData);
        List<ISyncedDTO> GetRemote();
        List<ISyncedDTO> GetRemote(DateTime lastSyc);
        ISyncedDTO GetRemote(int mediaRackId);
        bool CheckConnected();
    }
}
