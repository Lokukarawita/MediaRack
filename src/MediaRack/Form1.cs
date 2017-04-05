using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatTmdb.V3;

namespace MediaRack
{

    public partial class Form1 : Form
    {
  
        public Form1()
        {
            InitializeComponent();

            SelectedMovie = null;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Get Movie Details to Form2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listbx_search_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (listbx_search.SelectedIndex > -1)
            {
               
                MovieResult cmr = (MovieResult)listbx_search.SelectedItem;
                TmdbMovie mv = TmdbClient.Tmdb.GetMovieInfo(cmr.id);
                //TmdbMovieCast mc = TmdbClient.Tmdb.GetMovieCast(cmr.id);
                //TmdbMovieImages imgs = TmdbClient.Tmdb.GetMovieImages(mv.id);


                this.SelectedMovie = mv;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
               
            }
        }

        private void listbx_search_MouseClick(object sender, MouseEventArgs e)
        {

        }

        public TmdbMovie SelectedMovie { get; private set; }

        private void btn_pic_srch_Click(object sender, EventArgs e)
        {
            if (txt_search.Text.Length == 0)
            {
                MessageBox.Show("Enter Movie Name   !");
            }
            else
            {
                TmdbMovieSearch search = TmdbClient.Tmdb.SearchMovie(txt_search.Text, 1);

                listbx_search.DataSource = search.results;
                listbx_search.DisplayMember = "title";
            }
        }

      

       
    }
}
