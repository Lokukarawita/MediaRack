using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRack.Core.Util.Hash;

namespace MediaRack.Core.Data.Common
{
    [Serializable]
    public class MediaEntryDTO : BaseDTO, ISyncedDTO
    {
        public MediaEntryDTO()
        {
            this.MediaType = MediaType.Movie;
            this.LocalStatus = LocalSyncStatus.NEW;
            this.Series = string.Empty;
            this.Episode = string.Empty;
        }

        public int ID { get; set; }

        //Info Metadata
        public int TmdbID { get; set; }
        public int TvdbID { get; set; }
        public string ImdbID { get; set; }

        //Movie/Tv Metadata
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string Plot { get; set; }
        public double Rating { get; set; }
        public MediaType MediaType { get; set; }
        public string Series { get; set; }
        public string Episode { get; set; }

        //Media/File Metadata
        public MediaQuality QualityTags { get; set; }
        public string FileName { get; set; }
        public string AbsolutePath { get; set; }
        public string RootRelativePath { get; set; }
        public string Root { get; set; }
        public string PcName { get; set; }

        public virtual string Hash { get; set; }
        public virtual LocalSyncStatus LocalStatus { get; set; }
        public string GetHash()
        {
            StringBuilder sb = new StringBuilder();
            var props = GetType().GetProperties();
            foreach (var pr in props)
            {
                if (pr.CanRead)
                {
                    sb.Append(pr.GetValue(this, null)).Append("|");
                }
            }
            return sb.ToString().Hash();

        }
    }
}
