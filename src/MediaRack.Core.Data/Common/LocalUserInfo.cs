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

        public virtual string Username{get;set;}

        public virtual string Password { get; set; }

        public virtual UserSettingsMetaInfo Settings { get; set; }

        #region ISyncedDTO
        public virtual LocalSyncStatus LocalStatus { get; set; }

        public virtual DateTime Timestamp{ get; set; }
        #endregion
    }
}
