using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatTmdb.V3;
using System.Net;
using System.Drawing;
using System.IO;

namespace MediaRack
{
    public delegate void FileDownloadProgress(int percent); //progress delegate
    public delegate void FilesDownloadComplete(Image poster, Image backdrop); //complete delegate
    
    public class ImageDownloader
    {
        private string c = null; // cover url
        private string b = null; //backdrop url
        private WebClient webclient;

        private Image cover; //downloaded cover image
        private Image backdrop; //downloaded backdrop image
        private int currentDownloadItem = 0; // 0 is Cover 1 is Backdrop
        private decimal currentTotalProgress = 0;
        private decimal coverPcnt = 0; // % of cover download
        private decimal backdropPcnt = 0; // % of backdrop download
       
        //public events ---------------------------------------
        public event FilesDownloadComplete AllFilesDownloaded;
        public event FileDownloadProgress ProgressChanged;
        //-----------------------------------------------------

        public ImageDownloader()
        {
            webclient = new WebClient();
            webclient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(c_DownloadDataCompleted);
            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(c_DownloadProgressChanged);
        }

        private void c_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (currentDownloadItem == 0)
            {
                coverPcnt = (decimal)e.ProgressPercentage;
            }
            else
            {
                backdropPcnt = (decimal)e.ProgressPercentage;
            }

            decimal tpcnt = (coverPcnt + backdropPcnt);
            currentTotalProgress = (tpcnt / (decimal)2);


            if (ProgressChanged != null)
            {
                ProgressChanged.DynamicInvoke((int)currentTotalProgress);
            }
        }
        private void c_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (currentDownloadItem == 0)
            {
                currentDownloadItem = 1;
                cover = Image.FromStream(new MemoryStream(e.Result));
                webclient.DownloadDataAsync(new Uri(b), null);
            }
            else
            {
                backdrop = Image.FromStream(new MemoryStream(e.Result));
                currentDownloadItem = 0;
                if (AllFilesDownloaded != null)
                    AllFilesDownloaded(cover, backdrop);
                IsWorking = false;
            }
        }

        public void Download(string poster, string backdrop)
        {
            try
            {
                this.c = poster;
                this.b = backdrop;
                currentDownloadItem = 0;
                currentTotalProgress = 0;
                webclient.DownloadDataAsync(new Uri(c), null);
                IsWorking = true;
            }
            catch (Exception) { IsWorking = false; }

        }
        public bool IsWorking { get; private set; }
    }
}
