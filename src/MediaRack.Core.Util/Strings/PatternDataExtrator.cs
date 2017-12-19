using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Strings
{
    public static class PatternDataExtrator
    {
        private const string PATTERN_REGEX = @"[a-z0-9|]+";

        public static Dictionary<string, string> ExtractPathData(this string path, string pattern)
        {
            var rturn = new Dictionary<string, string>();

            //Patterns to extract
            var mat = Regex.Matches(pattern, PATTERN_REGEX, RegexOptions.IgnoreCase);

            var path_segments = path.Split(Path.DirectorySeparatorChar);
            Array.Reverse(path_segments);
            if (path_segments.Length > 1)
            {
                var parent_fld_segment = path_segments[1];
                var srb = new StringBuilder();
                var data = new List<string>();

                //Extract data
                foreach (var item in parent_fld_segment)
                {
                    if (Regex.IsMatch(item.ToString(), @"[^a-z0-9_\s|]+", RegexOptions.IgnoreCase))
                    {
                        if (pattern.IndexOf(item) > -1)
                        {

                            var cur = srb.ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(cur))
                                data.Add(cur);
                            srb.Clear();
                        }
                    }
                    else
                    {
                        srb.Append(item);
                    }
                }

                for (int i = 0; i < mat.Count; i++)
                {
                    if (i >= data.Count) { rturn.Add(mat[i].Value, string.Empty); continue; }

                    rturn.Add(mat[i].Value, data[i]);
                }
            }

            return rturn;
        }

        public static string[] ToFolderPatterns(this string x)
        {
            var mat = Regex.Matches(x, PATTERN_REGEX, RegexOptions.IgnoreCase);
            return mat.Cast<Match>().Select(m => m.Value).ToArray();
        }

        public static string ExtractMovieName(this string path)
        {
            var path_segments = path.Split(Path.DirectorySeparatorChar);
            Array.Reverse(path_segments);
            if (path_segments.Length > 1)
            {
                var fileName_segment = path_segments[0];

                var movie_name = fileName_segment.Replace('.', ' ');

                return movie_name;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
