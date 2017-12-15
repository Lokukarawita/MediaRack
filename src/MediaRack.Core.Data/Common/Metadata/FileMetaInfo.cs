using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class FileMetaInfo : MetaInfo
    {
        public FileMetaInfo()
        {
            Subtitles = new List<SubtitleMetaInfo>();
            QualityTags = new HashSet<MediaQuality>();
        }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("subtitles")]
        public List<SubtitleMetaInfo> Subtitles { get; set; }

        [JsonProperty("quality", ItemConverterType = typeof(StringEnumConverter))]
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

        [JsonProperty("md5")]
        public string MD5Hash { get; set; }
    }
}
