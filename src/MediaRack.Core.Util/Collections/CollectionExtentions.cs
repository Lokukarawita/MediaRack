using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Collections
{
    public static class CollectionExtentions
    {
        public static T GetValue<K, T>(this Dictionary<K, T> d, K key, T defaultResult)
        {
            if (d.ContainsKey(key))
                return d[key];
            else
                return defaultResult;
        }
    }
}
