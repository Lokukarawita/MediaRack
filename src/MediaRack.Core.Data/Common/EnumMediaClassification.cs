using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common
{
    /// <summary>
    /// Type of media
    /// </summary>
    public enum MediaClassification : int
    {
        /// <summary>
        /// Movie
        /// </summary>
        Movie,
        /// <summary>
        /// Tv
        /// </summary>
        Tv,
        /// <summary>
        /// Rated TV
        /// </summary>
        RTV,
        /// <summary>
        /// Rated Movie
        /// </summary>
        RMovie,
    }
}
