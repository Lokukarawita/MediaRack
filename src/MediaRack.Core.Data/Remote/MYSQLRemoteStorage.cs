using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MediaRack.Core.Data.Remote
{
    public class MYSQLRemoteStorage : RemoteStorage
    {
        public override bool CheckAvailability(string username)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string oldpwd, string newpwd)
        {
            throw new NotImplementedException();
        }

        public override bool SignUp(string username, string password, Common.Metadata.UserSettingsMetaInfo settings)
        {
            throw new NotImplementedException();
        }

        public override bool Connect()
        {
            throw new NotImplementedException();
        }

        public override MediaRackUser Authorize(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override void Disconnect()
        {
            throw new NotImplementedException();
        }

        public override bool CheckConnection()
        {
            throw new NotImplementedException();
        }

        public override bool UpdateUserSettings(Common.Metadata.UserSettingsMetaInfo settings)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRemote(List<Common.ISynchronizable> localData)
        {
            throw new NotImplementedException();
        }

        public override List<Common.ISynchronizable> GetRemote(Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override List<Common.ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override Common.ISynchronizable GetRemote(int mediaRackId, Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override bool HasRemoteChanges(DateTime lastUpdatedTimestamp)
        {
            throw new NotImplementedException();
        }

        public override bool IsAuthorized
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }
    }
}
