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
        public string Title { get; set; }

        public int Year { get; set; }

        public string Plot { get; set; }


        public virtual string Hash
        {
            get { return string.Format("{0}|{1}", Title, Plot).Hash(); }
        }

        public virtual LocalSyncStatus LocalStatus { get; set; }
    }
}
