using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Remote
{
    public interface IRemoteStorage
    {
        LocalUserInfo Connect(string username, string password);

        bool ChangePassword(string oldpwd, string newpwd);

        void Disconnect();

        void CheckConnection();


        void UpdateRemote(List<ISynchronizable> localData);
        
        List<ISynchronizable> GetRemote(Type synchronizable);
        
        List<ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable);
        
        ISynchronizable GetRemote(int mediaRackId, Type synchronizable);
        
        bool HasRemoteChanges(DateTime lastUpdatedTimestamp);
    }
}
