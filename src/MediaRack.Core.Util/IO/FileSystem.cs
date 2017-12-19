using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.IO
{
    public static class FileSystem
    {
        /// <summary>
        /// List files based on a pattern(s)
        /// </summary>
        /// <param name="directory">Directory to list files</param>
        /// <param name="searchOption">Search option</param>
        /// <param name="patterns">List of patterns</param>
        /// <returns></returns>
        public static string[] ListFiles(string directory, SearchOption searchOption, params string[] patterns)
        {
            if (string.IsNullOrWhiteSpace(directory))
                return new string[0];
            if (!Directory.Exists(directory))
                return new string[0];
            else
            {

                if (patterns == null || patterns.Length > 0)
                {
                    return new string[0];
                }
                else
                {
                    var found = new System.Collections.Concurrent.ConcurrentDictionary<string, string[]>();
                    Parallel.ForEach(patterns, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, x =>
                    {
                        try
                        {
                            var files = Directory.GetFiles(directory, "*" + x, searchOption);
                            int tryCount = 0;
                            while (true)
                            {
                                if (tryCount > 5)
                                    break;

                                if (found.TryAdd(x, files))
                                    break;
                                else
                                {
                                    tryCount++;
                                }
                            }
                        }
                        catch (Exception) { }
                    });


                    return found.SelectMany(x => x.Value).ToArray();
                }
            }
        }
    }
}
