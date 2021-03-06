﻿using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;
using MediaRack.Core.Util.Configuration;
using MediaRack.Core.Util.Security;
using MediaRack.Core.Util.Net;
using MediaRack.Core.Util.Collections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public class MYSQLRemoteStorage : RemoteStorage
    {
        private MediaRackUser currentUser;
        private bool isauthorized;
        private MySqlConnection connection;
        private string remote_server;

        public MYSQLRemoteStorage()
        { InitConnection(); }

        public MYSQLRemoteStorage(TypeMap map)
            : base(map)
        { InitConnection(); }

        private void InitConnection()
        {
            //Collect settings
            var settings = new List<string>();
            remote_server = Config.GetValue(ConfigKeys.KEY_RDATA_SERVER, "localhost");
            settings.Add(string.Format("server={0}", remote_server));
            settings.Add(string.Format("database={0}", Config.GetValue(ConfigKeys.KEY_RDATA_DBNAME, "MediaRackData")));
            string port = Config.GetValue(ConfigKeys.KEY_RDATA_PORT, null);
            if (!string.IsNullOrWhiteSpace(port)) settings.Add(string.Format("port={0}", port));
            settings.Add(string.Format("uid={0}", Config.GetValue(ConfigKeys.KEY_RDATA_USER, "MediaRack")));
            settings.Add(string.Format("pwd={0}", Config.GetValue(ConfigKeys.KEY_RDATA_USER, "MYPASS")));
            settings.Add("old guids=true");

            //Build connection string
            var con_str = string.Join(";", settings.ToArray());

            //connection = new MySqlConnection("server=03d2348e-6414-46d2-a888-a7330079aa08.mysql.sequelizer.com;database=db03d2348e641446d2a888a7330079aa08;uid=svicvogivbqevucl;pwd=nR4iiYjHomeEgzsDiz4PZ4bUQh4TiqTwdMpPMT23QTXMCSmyThby4zKrQiAa5yy7");
            //connection = new MySqlConnection("server=localhost;database=db03d2348e641446d2a888a7330079aa08;uid=root;pwd=root");
            connection = new MySqlConnection(con_str);
        }


        private MySqlCommand GetRackQuery(Type synchronizable)
        {
            var mappedTo = currentMap.GetValue(synchronizable, null);
            mappedTo = mappedTo.Contains("{mrid}") ? mappedTo.Replace("{mrid}", currentUser.MediaRackUserID.ToString()) : mappedTo;
            return new MySqlCommand("SELECT * FROM " + mappedTo);
        }
        
        private MySqlCommand GetRackQuery(Type synchronizable, DateTime since)
        {
            var select = GetRackQuery(synchronizable);
            select.CommandText = select.CommandText + " WHERE Timestamp > @ts";
            select.Parameters.AddWithValue("@ts", since);
            return select;
        }

        private List<ISynchronizable> MapTo(Type synchronizable, DataTable dt)
        {
            if (synchronizable == typeof(MediaEntry))
            {
                var rtv = new List<ISynchronizable>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];
                    var entry = new Data.Common.MediaEntry()
                    {
                        MediaRackID = (int)row["id"],
                        Bookmark = (string)row["Bookmark"],
                        Classification = (MediaClassification)Enum.Parse(typeof(MediaClassification), (string)row["Classification"]),
                        Comment = (string)row["Comment"],
                        CompositionInfo = CompositionMetaInfo.FromJson<CompositionMetaInfo>(row["CompositionInfo"].ToString()),
                        Favorite = (bool)row["Favorite"],
                        FileInfo = FileCollectionMetaInfo.FromJson<FileCollectionMetaInfo>(row["FileInfo"].ToString()),
                        Grade = (string)row["Grade"],
                        IDInfo = IDMetaInfo.FromJson<IDMetaInfo>(row["IDInfo"].ToString()),
                        ImageCacheID = (string)row["ImageCacheID"],
                        LocalStatus = LocalSyncStatus.SYNCED,
                        Timestamp = (DateTime)row["Timestamp"],
                        Watched = (bool)row["Watched"],
                        WatchedOn = (DateTime)row["WatchedOn"]
                    };
                    rtv.Add(entry);
                }

                return rtv;
            }
            else
            {
                return null;
            }
        }



        public override bool CheckAvailability(string username)
        {
            if (IsConnected)
            {
                using (var cmd = new MySqlCommand("ACC_AVAIL", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("username", username).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("avail", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var rv = Convert.ToBoolean(cmd.Parameters["avail"].Value);
                        return true;
                    }
                    catch (Exception) { }
                }
            }
            return false;
        }

        public override bool ChangePassword(string oldpwd, string newpwd)
        {
            if (IsConnected)
            {
                if (IsAuthorized)
                {
                    using (var cmd = new MySqlCommand("CHANGE_PWD", connection))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("mrid", currentUser.MediaRackUserID).Direction = ParameterDirection.Input;
                            cmd.Parameters.AddWithValue("oldpasswrd", oldpwd.Hash()).Direction = ParameterDirection.Input;
                            cmd.Parameters.AddWithValue("newpasswrd", newpwd.Hash()).Direction = ParameterDirection.Input;
                            cmd.ExecuteNonQuery();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    throw new RemoteStorageAuthenticationException("Authorization required");
                }
            }
            else
            {
                throw new RemoteStorageException("Not connected");
            }
        }

        public override bool SignUp(string username, string password, UserSettingsMetaInfo settings)
        {
            if (IsConnected)
            {
                using (var cmd = new MySqlCommand("SIGN_UP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cmd.Parameters.AddWithValue("username", username).Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("passwrd", password.Hash()).Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("settings", settings != null ? settings.ToJson() : "{}").Direction = ParameterDirection.Input;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception) { }
                }
            }
            return false;
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
                using (var cmd = new MySqlCommand("AUTHENTICATE", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("usrname", username).Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("passwrd", password.Hash()).Direction = ParameterDirection.Input;
                        cmd.Parameters.AddWithValue("chktime", DateTime.UtcNow).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("mrid", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("lasts", MySqlDbType.DateTime).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("sett", MySqlDbType.LongText).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        var mrid = (int)cmd.Parameters["mrid"].Value;
                        if (mrid == -1)
                        {
                            throw new RemoteStorageAuthenticationException("Authentication failed");
                        }
                        else
                        {
                            currentUser = new MediaRackUser();
                            currentUser.Username = username;
                            currentUser.MediaRackUserID = mrid;
                            currentUser.LastSeen = DateTime.SpecifyKind((DateTime)cmd.Parameters["lasts"].Value, DateTimeKind.Utc);
                            currentUser.Settings = UserSettingsMetaInfo.FromJson<UserSettingsMetaInfo>((string)cmd.Parameters["sett"].Value);
                            currentUser.Password = "<BLOCKED>";
                            isauthorized = true;
                            return currentUser;
                        }
                    }
                    catch (RemoteStorageAuthenticationException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw new RemoteStorageException("Authorization error, Please contact the developers", ex);
                    }
                }
            }
            else
            {
                throw new RemoteStorageException("Not connected");
            }
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
            var intconn = NetworkHelper.Ping(remote_server);
            if (intconn)
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed && connection.State != System.Data.ConnectionState.Broken)
                {
                    return true;
                }
            }

            return false;
        }

        public override MediaRackUser GetCurrentUser()
        {
            if (IsConnected)
            {
                if (IsAuthorized)
                {
                    using (var qry = new MySqlCommand("SELECT * FROM users WHERE id=@1", connection))
                    {
                        using (var da = new MySqlDataAdapter(qry))
                        {
                            var data = new DataTable();
                            da.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                currentUser.Username = data.Rows[0]["Username"].ToString();
                                currentUser.MediaRackUserID = (int)data.Rows[0]["Id"];
                                currentUser.LastSeen = DateTime.SpecifyKind((DateTime)data.Rows[0]["LastSeen"], DateTimeKind.Utc);
                                currentUser.Settings = UserSettingsMetaInfo.FromJson<UserSettingsMetaInfo>(data.Rows[0]["Settings"].ToString());
                                currentUser.Password = "<BLOCKED>";
                                return currentUser;
                            }
                            else
                            {
                                return currentUser;
                            }
                        }
                    }
                }
                else
                {
                    throw new RemoteStorageAuthenticationException("Authorization required"); 
                }
            }
            else
            {
                throw new RemoteStorageException("Remote storage cannot be accessed at this moment");
            }
        }

        public override bool UpdateUserSettings(UserSettingsMetaInfo settings)
        {
            if (IsConnected)
            {
                if (IsAuthorized)
                {
                    using (var cmd = new MySqlCommand("SAVE_USERSETT", connection))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("mrid", currentUser.MediaRackUserID).Direction = ParameterDirection.Input;
                            cmd.Parameters.AddWithValue("sett", settings.ToJson()).Direction = ParameterDirection.Input;
                            cmd.ExecuteNonQuery();
                            currentUser.Settings = settings;
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    throw new RemoteStorageAuthenticationException("Authorization required");
                }
            }
            else
            {
                throw new RemoteStorageException("Not connected");
            }
        }

        public override List<ISynchronizable> GetRemote(Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override List<ISynchronizable> GetRemote(DateTime lastSyc, Type synchronizable)
        {
            if (IsConnected)
            {
                if (IsAuthorized)
                {
                    var cmd = GetRackQuery(synchronizable, lastSyc);
                    using (cmd)
                    {
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            da.Fill(dt);
                            return MapTo(synchronizable, dt);
                        }
                    }
                }
                else
                {
                    throw new RemoteStorageAuthenticationException("Authorization required");
                }
            }
            else
            {
                throw new RemoteStorageException("Remote storage cannot be accessed at this moment");
            }
        }

        public override ISynchronizable GetRemote(int mediaRackId, Type synchronizable)
        {
            throw new NotImplementedException();
        }

        public override bool HasRemoteChanges(DateTime lastUpdatedTimestamp)
        {
            throw new NotImplementedException();
        }

        public override RemoteSyncResult UpdateRemote(List<ISynchronizable> localData)
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
