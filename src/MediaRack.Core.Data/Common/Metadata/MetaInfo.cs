using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Data.Common.Metadata
{
    /// <summary>
    /// Base class for all MedaInfo classes
    /// </summary>
    [JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public abstract class MetaInfo
    {
        /// <summary>
        /// Convert serialized meta info string into MetaInfo object
        /// </summary>
        /// <typeparam name="T">Type of MetaInfo to be converted</typeparam>
        /// <param name="serialized">Serialized text</param>
        /// <returns></returns>
        public static T FromJson<T>(string serialized) where T : MetaInfo
        {
            return (T)JsonConvert.DeserializeObject(serialized, typeof(T));
        }

        /// <summary>
        /// Convert this MetaInfo object to a json string
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
