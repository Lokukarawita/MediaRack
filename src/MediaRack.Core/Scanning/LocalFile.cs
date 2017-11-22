using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Scanning
{
    public class LocalFile
    {
        public static LocalFile Create(string path)
        {
            return null;
        }

        public string Path { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }
    }
}
