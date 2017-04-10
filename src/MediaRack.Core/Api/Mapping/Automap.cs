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
    public class Automap
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TmdbMovie, CompositionMetaInfo>();
            });
        }
    }
}
