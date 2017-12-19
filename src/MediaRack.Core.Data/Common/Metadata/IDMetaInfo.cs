using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class IDMetaInfo : MetaInfo
    {
        public IDMetaInfo()
        {
            this.TrailerInfo = new List<TrailerMetaInfo>();
        }

        [JsonProperty("tmdb")]
        public int TmdbID { get; set; }
        
        [JsonProperty("tvdb")]
        public int TvdbID { get; set; }
        
        [JsonProperty("imdb")]
        public string ImdbID { get; set; }

        [JsonProperty("trailers")]
        public List<TrailerMetaInfo> TrailerInfo { get; set; }
    }
}
