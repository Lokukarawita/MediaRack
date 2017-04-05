using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatTmdb.V3;
using System.Drawing;
using System.Net;
using System.IO;

namespace mv_collect_db
{
    public class TmdbClient
    {
        private static Tmdb tmdb;
        private static TmdbConfiguration config;

        static TmdbClient()
        {
            tmdb = new Tmdb("500569e716c3a599be3b0b3f851092ad");
            config = tmdb.GetConfiguration();
        }

        public static Uri GetImageUri(string filepath, string width) 
        {
            return new Uri(config.images.base_url + width + filepath);
        }

        public static Image DownloadImage(string filepath)
        {
            Uri u = GetImageUri(filepath, "original");
            return DownloadImage(u);
        }
        public static Image DownloadImage(string filepath, int width) 
        {
            Uri u = GetImageUri(filepath, ("w" + width.ToString()));
            Image i = null;
            
            try
            {
                i = DownloadImage(u);
            }
            catch (Exception)
            {
                try
                {
                    u = GetImageUri(filepath, "original");
                    i = DownloadImage(u);
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return DownloadImage(u);
        }
        public static Image DownloadImage(Uri uri) 
        {
            WebClient c = new WebClient();
            byte[] data = c.DownloadData(uri);
            return Image.FromStream(new MemoryStream(data));
        }

        public static Tmdb Tmdb 
        { 
            get { return tmdb; } 
        }
        public static TmdbConfiguration Config
        {
            get { return config; }
        }
    }
}
