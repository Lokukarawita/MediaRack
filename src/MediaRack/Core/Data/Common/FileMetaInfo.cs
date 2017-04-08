using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    public class FileMetaInfo : MetaInfo
    {
        public FileMetaInfo()
        {
            QualityTags = new HashSet<MediaQuality>();
        }

        [JsonProperty("quality")]
        public HashSet<MediaQuality> QualityTags { get; set; }
        
        [JsonProperty("dimensions")]
        public Size Dimensions { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("absPath")]
        public string AbsolutePath { get; set; }
        
        [JsonProperty("rootRPath")]
        public string RootRelativePath { get; set; }
        
        [JsonProperty("root")]
        public string Root { get; set; }
        
        [JsonProperty("pcName")]
        public string PcName { get; set; }
    }
}
