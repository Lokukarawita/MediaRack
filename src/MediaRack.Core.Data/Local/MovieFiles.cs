using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MediaRack.Core.Data.Local
{
    public class MovieFiles
    {
        private static string image_base_path = string.Empty;
        private static string plot_base_path = string.Empty;

        static MovieFiles()
        {
            plot_base_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"mv_plot");
            if (!Directory.Exists(plot_base_path))
            {
                Directory.CreateDirectory(plot_base_path);
            }
            
            image_base_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"mv_img");
            if (!Directory.Exists(image_base_path))
            {
                Directory.CreateDirectory(image_base_path);
            }
        }

        public static void SavePlot(int movieid, string plot)
        {
            string fn = movieid.ToString() + ".mpl";
            fn = Path.Combine(plot_base_path, fn);
            StreamWriter sr = new StreamWriter(fn, false, Encoding.UTF8);
            sr.WriteLine(plot);
            sr.Close();
        }
        public static string ReadPlot(int movieid)
        {
            string plot = string.Empty;

            string fn = movieid.ToString() + ".mpl";
            fn = Path.Combine(plot_base_path, fn);

            StreamReader sr = new StreamReader(fn, Encoding.UTF8);
            plot = sr.ReadToEnd();
            sr.Close();

            return plot;
        }
        public static void DeletePlot(int movieid)
        {
            string fn = movieid.ToString() + ".mpl";
            fn = Path.Combine(plot_base_path, fn);

            if (File.Exists(fn))
            {
                File.Delete(fn);
            }
        }

        public static void SaveCoverImage(int movieid, Image i)
        {
            string fn = movieid.ToString() + "_C.img";
            fn = Path.Combine(image_base_path, fn);
            i.Save(fn, ImageFormat.Png);
        }
        public static Image GetCoverImage(int movieid)
        {
            string fn = movieid.ToString() + "_C.img";
            fn = Path.Combine(image_base_path, fn);
            return Image.FromFile(fn);
        }
        public static void DeleteCoverImage(int movieid)
        {
            string fn = movieid.ToString() + "_C.img";
            fn = Path.Combine(image_base_path, fn);
            
            if (File.Exists(fn))
            {
                File.Delete(fn);
            }
        }

        public static void SaveBackImage(int movieid, Image i)
        {
            string fn = movieid.ToString() + "_B.img";
            fn = Path.Combine(image_base_path, fn);
            i.Save(fn, ImageFormat.Png);
        }
        public static Image GetBackImage(int movieid)
        {
            string fn = movieid.ToString() + "_B.img";
            fn = Path.Combine(image_base_path, fn);
            return Image.FromFile(fn);
        }
        public static void DeleteBackImage(int movieid)
        {
            string fn = movieid.ToString() + "_B.img";
            fn = Path.Combine(image_base_path, fn);

            if (File.Exists(fn))
            {
                File.Delete(fn);
            }
        }
    }
}
