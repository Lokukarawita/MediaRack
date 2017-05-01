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
    public class TmdbReleaseDateResolver : IValueResolver<TmdbMovie, CompositionMetaInfo, DateTime>
    {
        public DateTime Resolve(TmdbMovie source, CompositionMetaInfo destination, DateTime destMember, ResolutionContext context)
        {
            if (source == null)
            {
                if (destination != null) destination.Released = DateTime.MinValue;
                return DateTime.MinValue;
            }
            else
            {
                try
                {
                    var dt = source.release_date;
                    var parsed = DateTime.ParseExact(dt, "yyyy-MM-dd", null);
                    if (destination != null) destination.Released = parsed;
                    return parsed;
                }
                catch (Exception)
                {
                    if (destination != null) destination.Released = DateTime.MinValue;
                    return DateTime.MinValue;
                }
            }
        }
    }
}
