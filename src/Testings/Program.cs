using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Testings
{
    public static class EnumExtentions
    {
        public static string GetDescription(this Enum e)
        {
            var type = e.GetType();
            var members = type.GetMember(e.ToString());
            if (members != null && members.Length > 0)
            {
                var attribs = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs != null && attribs.Length > 0)
                {
                    return ((DescriptionAttribute)attribs[0]).Description;
                }
            }

            return e.ToString();
        }
    }

    class Program
    {
        enum QLT
        {
            [Description("A Enum")]
            A,
            B,
            C,
            D
        }

        static void Main(string[] args)
        {

            var str = QLT.B.GetDescription();

            //HashSet<QLT> s = new HashSet<QLT>();
            //s.Add(QLT.A);
            //s.Add(QLT.B);
            //s.Add(QLT.A);
            //s.Add(QLT.D);

            //var str = JsonConvert.SerializeObject(s);
            //var de = JsonConvert.DeserializeObject<HashSet<QLT>>(str);
        }
    }
}
