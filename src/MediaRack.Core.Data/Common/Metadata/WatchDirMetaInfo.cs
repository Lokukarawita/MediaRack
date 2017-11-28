using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaRack.Core.Util.Configuration;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class WatchDirMetaInfo : MetaInfo
    {
        public WatchDirMetaInfo()
        {
            FolderStructureSeq = ConfigKeys.KEY_FILPROC_DEFAULTFOLDERSEQ.GetConfigValue<string>(@"|movie_name| (|year|) [|quality|]\|filename_unformatted|");
        }

        [JsonProperty("pcName")]
        public string PCName { get; set; }
        [JsonProperty("directory")]
        public string Directory { get; set; }
        [JsonProperty("lastChecked")]
        public DateTime LastChecked { get; set; }
        [JsonProperty("fldrStrucSeq")]
        public string FolderStructureSeq { get; set; }
    }
}
