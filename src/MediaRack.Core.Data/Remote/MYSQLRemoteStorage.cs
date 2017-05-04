using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;
using MediaRack.Core.Util.Net;
using System.Data;

namespace MediaRack.Core.Data.Remote
{
    public class MYSQLRemoteStorage : RemoteStorage
    {
        private bool isauthorized;
        private MySqlConnection connection;

        public MYSQLRemoteStorage()
        { InitConnection(); }

        public MYSQLRemoteStorage(TypeMap map)
            : base(map)
        { InitConnection(); }

        private void InitConnection()
        {
            connection = new MySqlConnection("server=03d2348e-6414-46d2-a888-a7330079aa08.mysql.sequelizer.com;database=db03d2348e641446d2a888a7330079aa08;uid=svicvogivbqevucl;pwd=nR4iiYjHomeEgzsDiz4PZ4bUQh4TiqTwdMpPMT23QTXMCSmyThby4zKrQiAa5yy7");
        }



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
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override MediaRackUser Authorize(string username, string password)
        {
            if (IsConnected)
            {
                using (var da = new MySqlDataAdapter("SELECT * FROM Users WHERE Username = @1", connection))
                {
                    da.SelectCommand.Parameters.AddWithValue("@1", username);
                    var dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        throw new RemoteStorageAuthenticationException("Login failed, Cannot find the account");

                }
            }

            return new MediaRackUser();
        }

        public override void Disconnect()
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception) { }
            }
        }

        public override bool CheckConnection()
        {
            var intconn = NetworkHelper.HasInternetConnection();
            if (intconn)
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed && connection.State != System.Data.ConnectionState.Broken)
                {
                    return true;
                }
            }

            return false;
        }



        public override bool UpdateUserSettings(UserSettingsMetaInfo settings)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRemote(List<ISynchronizable> localData)
        {
            throw new NotImplementedException();
        }

        public override List<ISynchronizable> GetRemote(Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override List<ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override ISynchronizable GetRemote(int mediaRackId, Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override bool HasRemoteChanges(DateTime lastUpdatedTimestamp)
        {
            throw new NotImplementedException();
        }



        public override bool IsAuthorized
        {
            get { return isauthorized; }
        }

        public override bool IsConnected
        {
            get { return CheckConnection(); }
        }
    }
}
