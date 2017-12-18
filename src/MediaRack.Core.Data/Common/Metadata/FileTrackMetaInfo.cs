using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MediaRack.Core.Data.Common.Metadata
{
    public class FileTrackMetaInfo : MetaInfo
    {
        [JsonProperty("id")]
        public int TrackID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FileTrackType TrackType { get; set; }
    }
}
