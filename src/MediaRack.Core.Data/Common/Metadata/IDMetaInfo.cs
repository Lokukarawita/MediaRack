using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MediaRack.Core.Data.Common.Metadata
{
    public class IDMetaInfo : MetaInfo
    {
        [JsonProperty("tmdb")]
        public int TmdbID { get; set; }
        
        [JsonProperty("tvdb")]
        public int TvdbID { get; set; }
        
        [JsonProperty("imdb")]
        public string ImdbID { get; set; }
        
        [JsonProperty("youtube")]
        public string YoutubeID { get; set; }
    }
}
