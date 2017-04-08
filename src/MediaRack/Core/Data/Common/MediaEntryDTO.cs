using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRack.Core.Util.Enums;

namespace MediaRack.Core.Data.Common
{
    [Serializable]
    public class MediaEntryDTO : BaseDTO, ISyncedDTO
    {
        public MediaEntryDTO()
        {
            this.Classification = MediaClassification.Movie;
            this.LocalStatus = LocalSyncStatus.NEW;
            this.IDInfo = new IDMetaInfo();
            this.CompositionInfo = new CompositionMetaInfo();
            this.FileInfo = new FileMetaInfo();
        }

        #region MediaRack
        public int MediaRackID { get; set; }

        public MediaClassification Classification { get; set; } 
        #endregion

        /// <summary>
        /// ID information for other web sites
        /// </summary>
        public IDMetaInfo IDInfo { get; set; }

        /// <summary>
        /// Move or Tv series info
        /// </summary>
        public CompositionMetaInfo CompositionInfo { get; set; }

        /// <summary>
        /// File and quality information
        /// </summary>
        public FileMetaInfo FileInfo { get; set; }

        #region ISyncedDTO
        public virtual DateTime Timestamp { get; set; }

        public virtual LocalSyncStatus LocalStatus { get; set; }

        #endregion
    }
}
