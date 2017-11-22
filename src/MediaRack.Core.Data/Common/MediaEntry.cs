using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaRack.Core.Util.Enums;
using MediaRack.Core.Data.Common.Metadata;

namespace MediaRack.Core.Data.Common
{
    [Serializable]
    public class MediaEntry : BaseEntity, ISynchronizable
    {
        private bool watched = false;
        private DateTime watchedon = DateTime.MinValue;

        public MediaEntry()
        {
            this.Classification = MediaClassification.Movie;
            this.LocalStatus = LocalSyncStatus.NEW;
            this.IDInfo = new IDMetaInfo();
            this.CompositionInfo = new CompositionMetaInfo();
            this.FileInfo = new FileCollectionMetaInfo();
        }

        #region MediaRack
        /// <summary>
        /// Id for the local rack
        /// </summary>
        public virtual int LocalRackID { get; set; }
        
        /// <summary>
        /// MediaRack id
        /// </summary>
        public virtual int MediaRackID { get; set; }

        /// <summary>
        /// MediaRack classification
        /// </summary>
        public virtual MediaClassification Classification { get; set; }

        /// <summary>
        /// Has wacted this media element
        /// </summary>
        public virtual bool Watched
        {
            get { return this.watched; }
            set
            {
                this.watched = value;
                this.watchedon = value ? DateTime.UtcNow : DateTime.MinValue;
            }
        }

        /// <summary>
        /// Date watched on
        /// </summary>
        public virtual DateTime WatchedOn
        {
            get { return watchedon; }
            set { watchedon = value; }
        }

        /// <summary>
        /// Custom grade
        /// </summary>
        public virtual string Grade { get; set; }

        /// <summary>
        /// Comment by user
        /// </summary>
        public virtual string Comment { get; set; }

        /// <summary>
        /// Custom bookmark
        /// </summary>
        public virtual string Bookmark { get; set; }

        /// <summary>
        /// Is Favorite
        /// </summary>
        public virtual bool Favorite { get; set; }

        /// <summary>
        /// Local image cache id
        /// </summary>
        public virtual string ImageCacheID { get; set; }
        #endregion

        #region Metadata
        /// <summary>
        /// ID information for other web sites
        /// </summary>      
        public virtual IDMetaInfo IDInfo { get; set; }

        /// <summary>
        /// Move or Tv series info
        /// </summary>
        public virtual CompositionMetaInfo CompositionInfo { get; set; }

        /// <summary>
        /// Files and quality information
        /// </summary>
        public virtual FileCollectionMetaInfo FileInfo { get; set; }
        #endregion

        #region ISyncedDTO
        /// <summary>
        /// Last updated
        /// </summary>
        public virtual DateTime Timestamp { get; set; }

        /// <summary>
        /// Sync status
        /// </summary>
        public virtual LocalSyncStatus LocalStatus { get; set; }
        #endregion
    }
}
