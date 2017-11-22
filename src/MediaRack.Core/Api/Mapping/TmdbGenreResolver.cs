using AutoMapper;
using MediaRack.Core.Data.Common.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatTmdb.V3;

namespace MediaRack.Core.Api.Mapping
{
    internal class TmdbGenreResolver : AutoMapper.IValueResolver<TmdbMovie, CompositionMetaInfo, List<string>>
    {
        public List<string> Resolve(TmdbMovie source, CompositionMetaInfo destination, List<string> destMember, ResolutionContext context)
        {
            if (source == null && (source.genres == null || source.genres.Count == 0))
            {
                return new List<string>();

            }
            else
            {
                var list = source.genres.Select(x => x.name).ToList();
                if (destination != null) destination.Genres = list;
                return list;
            }

        }
    }
}
