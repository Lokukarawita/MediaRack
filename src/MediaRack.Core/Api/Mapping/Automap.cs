using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WatTmdb.V3;
using MediaRack.Core.Data.Common.Metadata;

namespace MediaRack.Core.Api.Mapping
{
    public static class Automap
    {
        static Automap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TmdbMovie, CompositionMetaInfo>()
                    .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.title))
                    .ForMember(dst => dst.Overview, opt => opt.MapFrom(src => src.overview))
                    .ForMember(dst => dst.Rating, opt => opt.MapFrom(src => src.vote_average))
                    .ForMember(dst => dst.Tagline, opt => opt.MapFrom(src => src.tagline))
                    .ForMember(dst => dst.Runtime, opt => opt.MapFrom(src => src.runtime))
                    //.ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.genres.Select(x => x.name).ToList()))
                    .ForMember(dst => dst.Genres, opt => opt.ResolveUsing<TmdbGenreResolver>())
                    .ForMember(dst => dst.Released, opt => opt.ResolveUsing<TmdbReleaseDateResolver>());
            });
        }


        public static CompositionMetaInfo ToComposition(this TmdbMovie movie)
        {
            return Mapper.Map<CompositionMetaInfo>(movie);
        }
    }
}
