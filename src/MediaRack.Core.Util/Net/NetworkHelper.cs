using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Util.Net
{
    public class NetworkHelper
    {
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetCheckConnection(string lpszUrl, int dwFlags, int dwReserved);

        public static bool HasInternetConnection()
        {
            return NetworkHelper.InternetCheckConnection("http://www.google.com", 1, 0);
        }
    }
}
