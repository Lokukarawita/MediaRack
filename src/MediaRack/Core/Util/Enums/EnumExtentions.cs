using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MediaRack.Core.Util.Enums
{
    public static class EnumExtentions
    {
        /// <summary>
        /// Get description attribute value from a enum member
        /// </summary>
        /// <param name="e">Enum member</param>
        /// <returns>Description attribute value or string representation of Enum member</returns>
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
}
