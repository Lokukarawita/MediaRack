using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class FileCollectionMetaInfo : MetaInfo
    {
        public FileCollectionMetaInfo()
        {
            Files = new List<FileMetaInfo>();
        }

        [JsonProperty("files")]
        public List<FileMetaInfo> Files { get; set; }
    }
}
