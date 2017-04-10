using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MediaRack
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MediaRack.Core.Api.TMDDb.TmdbClient.API_KEY = Properties.Settings.Default.APIKEY_TMDB;
            Application.Run(new FrmMain());
        }
    }
}
