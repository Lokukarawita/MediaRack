using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    public class LocalUserInfo : BaseEntity, IUserInfoFields, ISynchronizable
    {
        public LocalUserInfo()
        {
            LocalStatus = LocalSyncStatus.NEW;
            Timestamp = DateTime.UtcNow;
            Settings = new UserSettingsMetaInfo();
        }

        public string Username{get;set;}

        public string Password { get; set; }

        public UserSettingsMetaInfo Settings { get; set; }

        #region ISyncedDTO
        public LocalSyncStatus LocalStatus { get; set; }

        public DateTime Timestamp{ get; set; }
        #endregion
    }
}
