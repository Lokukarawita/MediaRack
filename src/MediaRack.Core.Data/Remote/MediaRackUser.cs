using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Remote
{
    public class MediaRackUser : IUserInfoFields
    {
        public MediaRackUser()
        {
            Settings = new UserSettingsMetaInfo();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserSettingsMetaInfo Settings { get; set; }

        public int MediaRackUserID { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
