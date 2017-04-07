using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common.Mapping
{
    public static class MappingExtensions
    {

        public static void MapFrom(this MediaEntryDTO entry, WatTmdb.V3.TmdbMovie movie)
        {
            //entry.Title = movie.title;
            //entry.Year = DateTime.ParseExact(movie.release_date, "yyyy-MM-dd", null);
            //entry.X = movie.
            
            //entry.TmdbID = movie.id;
            //entry.ImdbID = movie.imdb_id;
            
        }
    }
}
