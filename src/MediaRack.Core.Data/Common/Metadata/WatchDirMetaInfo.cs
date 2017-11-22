using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class WatchDirMetaInfo : MetaInfo
    {
        [JsonProperty("pcName")]
        public string PCName { get; set; }
        [JsonProperty("directory")]
        public string Directory { get; set; }
        [JsonProperty("lastChecked")]
        public DateTime LastChecked { get; set; }
    }
}
