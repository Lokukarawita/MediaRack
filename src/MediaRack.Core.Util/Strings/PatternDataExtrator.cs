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
        private const string FN_IGNORE_PATTERN = @"x\d\d\d|bluray|brrip|webrip|wbrip|dvdrip|\dch|\d\.\dch|\d\.\d\sch|hevc|xvid|divx|hdtv|pdtv|webdl";
        private const string PATTERN_REGEX = @"[a-z0-9|]+";

        public static Dictionary<string, string> ExtractDataByFolder(this string path, string pattern)
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

        public static Dictionary<string, string> ExtractDataByFileName(this string fileName)
        {
            var rturn = new Dictionary<string, string>();

            //clone input
            string input = string.Copy(fileName);

            input = input.Replace(".", " ");
            //Remove text in brackets (){}[]<>
            input = Regex.Replace(input, @"\[.*?\]|\(.*?\)|\{.*?\}|\<.*?\?", string.Empty);
            //Remove quality level 1080p 720p 4k
            input = Regex.Replace(input, @"\d{3,4}p|\d{1}k", string.Empty, RegexOptions.IgnoreCase);
            //Remove special characters
            input = Regex.Replace(input, @"[-_&%#@!`~*,=+]{1}", string.Empty);
            //Remove other key words e.g.: x264, BRRIP
            input = Regex.Replace(input, FN_IGNORE_PATTERN, string.Empty, RegexOptions.IgnoreCase);
            //Remove anything after the first 4 digit match
            input = Regex.Replace(input, @"(?<=\d{4}).+", string.Empty);
            //Remove two or more consecutive spaces
            input = Regex.Replace(input, @"\s{2,}", " ", RegexOptions.IgnoreCase);

            
            var ptrn_moviename = new Regex(@".+(?=\d{4})");
            var ptrn_year = new Regex(@"\d{4}\z");

            //Movie name
            if (ptrn_moviename.IsMatch(input))
            {
                var data = ptrn_moviename.Match(input).Value;
                rturn.Add("movie_name", data);
            }
            else
            {
                rturn.Add("movie_name", string.Empty);
            }

            if (ptrn_year.IsMatch(input))
            {
                var data = ptrn_year.Match(input).Value;
                rturn.Add("movie_year", data);
            }
            else
            {
                rturn.Add("movie_year", string.Empty);
            }

            return rturn;
        }

        public static string[] ToFolderPatterns(this string x)
        {
            var mat = Regex.Matches(x, PATTERN_REGEX, RegexOptions.IgnoreCase);
            return mat.Cast<Match>().Select(m => m.Value).ToArray();
        }
    }
}
