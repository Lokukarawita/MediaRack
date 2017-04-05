using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatTmdb.V3;
using System.Data.OleDb;
using System.Net;

namespace mv_collect_db
{
    public partial class Form2 : Form
    {
        TmdbMovieImages imgs;
        TmdbMovie movie = null;
        string mvid;
        string cstr = string.Empty;
        Image bimg = null;

        ImageDownloader imageDownloader;

        public Form2(TmdbMovie movie)
        {
            InitializeComponent();
            this.movie = movie; 
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
            imageDownloader = new ImageDownloader();
            imageDownloader.AllFilesDownloaded += new FilesDownloadComplete(imageDownloader_AllFilesDownloaded);
            imageDownloader.ProgressChanged += new FileDownloadProgress(imageDownloader_ProgressChanged);
            
            if (movie != null) this.SetFields();
        }

        private void imageDownloader_ProgressChanged(int percent)
        {
            toolStripProgressBar1.Value = percent;
            toolStripStatusLabel2.Text = string.Format("{0}%", percent);
        }
        private void imageDownloader_AllFilesDownloaded(Image poster, Image backdrop)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.Image = poster;
            this.bimg = backdrop;

            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel1.Visible = false;
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel2.Visible = false;
            toolStripStatusLabel2.Text = "0%";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_avgvote.BackColor = Color.Transparent;         
        }

        /// <summary>
        /// All Movie Information
        /// </summary>
        /// 
        private void SetFields()
        {
            pictureBox1.Image = Properties.Resources.loading;
           

            #region Movie Cast
            TmdbMovieCast mc = TmdbClient.Tmdb.GetMovieCast(movie.id);
           

            int maxc = (mc.cast.Count >= 5 ? 5 : mc.cast.Count);

            StringBuilder srbTodb = new StringBuilder();
            StringBuilder srbTolbl = new StringBuilder();

            for (int i = 0; i < maxc; i++)
            {
                if (i < 4)
                {
                    //cstr += mc.cast[i].name + " , ";
                    srbTodb.Append(mc.cast[i].name).Append(" , ");
                }
                else
                {
                    srbTodb.Append(mc.cast[i].name);
                }
                //-----------------------------------------------------
                if (i < 2)
                {
                    srbTolbl.Append(mc.cast[i].name).Append(" , ");
                }
                else if (i == 2)
                {
                    srbTolbl.Append(mc.cast[i].name).AppendLine();
                }
                else if (i == 3)
                {
                    srbTolbl.Append(mc.cast[i].name).Append(" , ");
                }
                else
                {
                    srbTolbl.Append(mc.cast[i].name);
                }
            }

            lbl_cast.Text = srbTolbl.ToString();
            cstr = srbTodb.ToString();
            #endregion

            #region Basic Movie Information
            mvid = movie.id.ToString();
            lbl_mvname.Text = movie.title;
        //    lbl_avgvote.Text = movie.vote_average.ToString();
            lbl_reledate.Text = movie.release_date.ToString(); 
            txt_plt.Text = movie.overview.ToString();
            lbl_runtime.Text = movie.runtime.ToString() + " min";


            lbl_avgvote.Text = movie.vote_average.ToString();
            #endregion

            #region Movie Image
            imgs = TmdbClient.Tmdb.GetMovieImages(movie.id);
            GetImage();
            #endregion

            #region Movie Genere
            string geners = string.Empty; ;
            for (int i = 0; i < movie.genres.Count; i++)
            {
                if (i < movie.genres.Count - 1)
                {
                    geners += movie.genres[i].name + " | ";
                }
                else
                {
                    geners += movie.genres[i].name;
                }
            }  
            lbl_genres.Text = geners;
            #endregion

            OleDbDataAdapter cm = new OleDbDataAdapter("SELECT DISTINCT mv_dvdno FROM mv_det ", DBConnection.Open());
            DataSet ds = new DataSet();
            cm.Fill(ds);

            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_dvdno.Items.Add(dt.Rows[i]["mv_dvdno"].ToString());   
            }
            DBConnection.Close();   
        }
        /// <summary>
        /// Download the cover(poster) and backdrop
        /// </summary>
        private void GetImage()
        {
            string posteruri = string.Empty, backdropuri = string.Empty;

            if (imgs.posters.Count > 0)
            {

                Poster bd = imgs.posters[0];
                posteruri = TmdbClient.GetImageUri(bd.file_path, "original").ToString();
            }
            if (imgs.backdrops.Count > 0)
            {
                Backdrop bd = imgs.backdrops[0];
                backdropuri = TmdbClient.GetImageUri(bd.file_path, "original").ToString();
            }

            toolStripProgressBar1.Visible = true;
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel2.Visible = true;

            toolStripStatusLabel1.Text = "Downloading images, Please wait!";
            imageDownloader.Download(posteruri, backdropuri);
        }

        /// <summary>
        /// Call To GetImage Method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetImage();
        }

        private void btn_pic_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_pic_save_Click(object sender, EventArgs e)
        {
            {


                if (cmb_dvdno.Text == "")
                {
                    //MessageBox.Show("DVD No Is Empty                           ");
                    MessageBox.Show("DVD No Is Empty", "Moviez", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (imageDownloader.IsWorking)
                    {
                        //MessageBox.Show("Downloading images, Please wait...              ");
                        MessageBox.Show("Downloading images, Please wait...", "Moviez", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        try
                        {



                            #region Save Basic Movie Info
                            OleDbCommand da = new OleDbCommand("INSERT INTO mv_det(mv_id, mv_name, mv_rdte, mv_avgvote, mv_act, mv_runtime, mv_genre, mv_dvdno) VALUES(@1, @2, @3, @4, @5, @6, @7, @8)", DBConnection.Open());
                            da.Parameters.AddWithValue("@1", mvid);
                            da.Parameters.AddWithValue("@2", lbl_mvname.Text);
                            da.Parameters.AddWithValue("@3", lbl_reledate.Text);
                            da.Parameters.AddWithValue("@4", lbl_avgvote.Text);
                            da.Parameters.AddWithValue("@5", cstr);
                            da.Parameters.AddWithValue("@6", lbl_runtime.Text);
                            da.Parameters.AddWithValue("@7", lbl_genres.Text);
                            da.Parameters.AddWithValue("@8", cmb_dvdno.Text);

                            da.ExecuteNonQuery();

                            DBConnection.Close();
                            #endregion




                            #region Save Plot
                            MovieFiles.SavePlot(movie.id, movie.overview);
                            #endregion

                            #region Save Cover Image
                            MovieFiles.SaveCoverImage(movie.id, pictureBox1.Image);
                            #endregion

                            if (bimg != null) MovieFiles.SaveBackImage(movie.id, bimg);


                            MessageBox.Show("Done !               ", "Moviez", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();

                        }



                        catch (OleDbException)
                        {
                            MessageBox.Show("Movie already exists !", "Moviez", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unkown Error !", "Moviez", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
