using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WatTmdb.V3;
using MediaRack.Core.Data.Common.Metadata;
using MediaRack.Core.Data.Common;

namespace MediaRack.Core.Api.Mapping
{
    public static class MappingExtention
    {
        static MappingExtention()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TmdbMovie, CompositionMetaInfo>()
                    .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.title))
                    .ForMember(dst => dst.Overview, opt => opt.MapFrom(src => src.overview))
                    .ForMember(dst => dst.Rating, opt => opt.MapFrom(src => src.vote_average))
                    .ForMember(dst => dst.Tagline, opt => opt.MapFrom(src => src.tagline))
                    .ForMember(dst => dst.Runtime, opt => opt.MapFrom(src => src.runtime))
                    
                    .ForMember(dst => dst.BackdropPath, opt => opt.MapFrom(src => src.backdrop_path))
                    .ForMember(dst => dst.PosterPath, opt => opt.MapFrom(src => src.poster_path))
                    .ForMember(dst => dst.Genres, opt => opt.ResolveUsing<TmdbGenreResolver>())
                    .ForMember(dst => dst.Released, opt => opt.ResolveUsing<TmdbReleaseDateResolver>());

//.ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.genres.Select(x => x.name).ToList()))
                cfg.CreateMap<Cast, PersonMetaInfo>()
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dst => dst.CreditedAs, opt => opt.MapFrom(src => src.character))
                    .ForMember(dst => dst.TmdbID, opt => opt.MapFrom(src => src.id))
                    .ForMember(dst => dst.Name, opt => opt.UseValue<PersonClassification>(PersonClassification.Cast));

                cfg.CreateMap<Crew, PersonMetaInfo>()
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dst => dst.CreditedAs, opt => opt.MapFrom(src => src.job))
                    .ForMember(dst => dst.TmdbID, opt => opt.MapFrom(src => src.id))
                    .ForMember(dst => dst.Name, opt => opt.UseValue<PersonClassification>(PersonClassification.Crew));
            });
        }


        /// <summary>
        /// Map WatTmdb.V3.TmdbMovie to CompositionMetaInfo
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public static CompositionMetaInfo ToComposition(this TmdbMovie movie)
        {
            return Mapper.Map<CompositionMetaInfo>(movie);
        }

        /// <summary>
        /// Map WatTmdb.V3.Cast to PersonMetaInfo
        /// </summary>
        /// <param name="cast"></param>
        /// <returns></returns>
        public static List<PersonMetaInfo> ToPerson(this List<Cast> cast)
        {
            return Mapper.Map<List<PersonMetaInfo>>(cast);
        }
        /// <summary>
        /// Map WatTmdb.V3.Crew to PersonMetaInfo
        /// </summary>
        /// <param name="crew"></param>
        /// <returns></returns>
        public static List<PersonMetaInfo> ToPerson(this List<Crew> crew)
        {
            return Mapper.Map<List<PersonMetaInfo>>(crew);
        }
    }
}
