using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class SubtitleMetaInfo : MetaInfo
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("isEmbedded")]
        public bool IsEmbedded { get; set; }

        [JsonProperty("fileName")]
        public string Filename { get; set; }
    }
}
