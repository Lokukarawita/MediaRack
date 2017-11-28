using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class PersonMetaInfo : MetaInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("creditedAs")]
        public string CreditedAs { get; set; }

        [JsonProperty("classification")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PersonClassification Classification { get; set; }

        [JsonProperty("tmdbId")]
        public int TmdbID { get; set; }
    }
}
