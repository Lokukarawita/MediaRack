using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MediaRack.Core.Data.Common
{

    public class CompositionMetaInfo : MetaInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("released")]
        public DateTime Released { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("series")]
        public string Series { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }
    }
}
