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
        void UpdateRemote(List<ISynchronizable> localData);
        List<ISynchronizable> GetRemote();
        List<ISynchronizable> GetRemote(DateTime lastSyc);
        ISynchronizable GetRemote(int mediaRackId);
        bool CheckConnected();
    }
}
