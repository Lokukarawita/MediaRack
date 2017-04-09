using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MediaRack.Core.Data.Common.Metadata
{

    public class CompositionMetaInfo : MetaInfo
    {

        public CompositionMetaInfo()
        {
            Genres = new List<string>();
        }

        //---- Common ----

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        //---- Movie ----

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("released")]
        public DateTime Released { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("runtime")]
        public int Runtime { get; set; }

        [JsonProperty("poster")]
        public string PosterPath { get; set; }

        [JsonProperty("backdrop")]
        public string BackdropPath { get; set; }

        //---- TV ----

        [JsonProperty("series")]
        public string Series { get; set; }

        [JsonProperty("episode")]
        public string Episode { get; set; }
    }
}
