using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Configuration
{
    public static class ConfigExtentions
    {
        public static string GetConfigValue(this string key, string defaultValue)
        {
            var x = System.Configuration.ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(x) ? defaultValue : x;
        }
        public static T GetConfigValue<T>(this string key, T defaultValue)
        {
            var x = System.Configuration.ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(x))
            {
                return (T)defaultValue;
            }
            else
            {
                if (typeof(T).IsEnum)
                {
                    try
                    {
                        var enm = Enum.Parse(typeof(T), x, true);
                        return (T)enm;
                    }
                    catch (Exception)
                    {
                        return (T)defaultValue;
                    }
                }
                else
                {
                    return (T)Convert.ChangeType(x, typeof(T));
                }
            }
        }
    }
}
