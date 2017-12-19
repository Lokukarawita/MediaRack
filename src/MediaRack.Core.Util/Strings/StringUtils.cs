using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Strings
{
    public static class StringUtils
    {
        public static bool IsEqual(string left, string right, bool igonreCase = true)
        {
            if (left != null)
                return left.Equals(right, igonreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
            else if (right != null)
                return right.Equals(left, igonreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
            else
                return left == right;
        }
        public static bool Contains(string word, string part, bool ignoreCase = true)
        {
            if (word != null)
            {
                StringComparison x = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
                return word.IndexOf(part, x) > -1;
            }
            else
            {
                return false;
            }
        }
    }
}
