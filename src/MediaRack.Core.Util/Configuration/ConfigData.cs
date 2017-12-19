using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Configuration
{
    public static class ConfigData
    {
        public static readonly string[] COMPATIBLE_VIDEOCONTAINER_TYPES = new string[]{ 
            ".3gp",
            ".3g2",
            ".asf",
            ".amv",
            ".avi",
            ".flv",
            ".m4v",
            ".mkv",
            ".mpg",
            ".mpeg",
            ".mpv",
            ".mpe",
            ".mpg",
            ".m2v",
            ".mpeg",
            ".mp4",
            ".m4p",
            ".m4v",
            ".ogv",
            ".mov",
            ".qt",
            ".yuv",
            ".rm",
            ".rmvb",
            ".svi",
            ".vob",
            ".webm",
            ".wmv"
        };
        public static readonly string[] COMPATIBLE_EXTERNAL_SUBFILES = new string[] {
            ".srt",
            ".smt",
            ".ssa",
            ".ass",
            ".vtt"
        };

        public static readonly string[] KNOWN_TAG_TYPES = new string[]
        {
            "|movie_name|", "|year|", "|quality|", "|filename_unformatted|"
        };
    }
}
