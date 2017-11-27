using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Configuration
{
    public static class ConfigKeys
    {
        //Local file
        public const string KEY_LOCALFILE_MAXTRYCOUNT = "LOCALFILE_MAXTRYCOUNT";

        //Scanning
        public const string KEY_DIR_SCANINTERVAL = "DIR_SCANINTERVAL";

        //File processing
        public const string KEY_FILPROC_QUEUECHECKINTERVAL = "FILPROC_QUEUECHECKINTERVAL";
        public const string KEY_FILPROC_DEFAULTFOLDERSEQ = "FILPROC_DEFAULTFOLDERSEQ";
    }
}
