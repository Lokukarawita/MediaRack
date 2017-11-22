using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public abstract class RemoteStorage : IRemoteStorage
    {
        protected TypeMap currentMap;


        public RemoteStorage()
        {
            currentMap = TypeMap.ReadMap(this.GetType());
        }

        public RemoteStorage(TypeMap map)
        {
            currentMap = map;
        }


        public abstract bool CheckAvailability(string username);

        public abstract bool ChangePassword(string oldpwd, string newpwd);

        public abstract bool SignUp(string username, string password, UserSettingsMetaInfo settings);

        public abstract bool Connect();

        public abstract MediaRackUser Authorize(string username, string password);

        public abstract void Disconnect();

        public abstract bool CheckConnection();

        public abstract bool UpdateUserSettings(Common.Metadata.UserSettingsMetaInfo settings);

        public abstract void UpdateRemote(List<Common.ISynchronizable> localData);

        public abstract List<ISynchronizable> GetRemote(Type synchronizable);

        public abstract List<ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable);

        public abstract ISynchronizable GetRemote(int mediaRackId, Type synchronizable);

        public abstract bool HasRemoteChanges(DateTime lastUpdatedTimestamp);


        public abstract bool IsAuthorized { get; }

        public abstract bool IsConnected { get;}



        RemoteSyncResult IRemoteStorage.UpdateRemote(List<ISynchronizable> localData)
        {
            throw new NotImplementedException();
        }
    }
}
