using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Remote
{
    public interface IRemoteStorage
    {
        //User actions

        bool CheckAvailability(string username);

        bool ChangePassword(string oldpwd, string newpwd);

        bool SignUp(string username, string password, UserSettingsMetaInfo settings);


        //Global actions

        bool Connect();

        MediaRackUser Authorize(string username, string password);

        void Disconnect();

        bool CheckConnection();


        //Authorized actions

        bool UpdateUserSettings(UserSettingsMetaInfo settings);

        RemoteSyncResult UpdateRemote(List<ISynchronizable> localData);

        List<ISynchronizable> GetRemote(Type synchronizable);

        List<ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable);

        ISynchronizable GetRemote(int mediaRackId, Type synchronizable);

        bool HasRemoteChanges(DateTime lastUpdatedTimestamp);

        MediaRackUser GetCurrentUser();

        //Properties

        bool IsAuthorized { get; }

        bool IsConnected { get; }
    }
}
