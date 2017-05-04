using MediaRack.Core.Data.Remote;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class UserSettingsMetaInfo : MetaInfo
    {
        public UserSettingsMetaInfo()
        {
            WatchDir = new List<string>();
            ConflictProtocol = ConflictResolution.KeepRemote;
        }

        [JsonProperty("watchDir")]
        public List<string> WatchDir { get; set; }

        [JsonProperty("conflictProtocol")]
        public ConflictResolution ConflictProtocol { get; set; }
    }
}
