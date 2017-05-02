using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common
{
    public interface IUserInfoFields
    {
        string Username { get; set; }
        string Password { get; set; }
        UserSettingsMetaInfo Settings { get; set; }
    }
}
