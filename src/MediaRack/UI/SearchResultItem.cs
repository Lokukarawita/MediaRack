using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaRack.UI
{
    public partial class SearchResultItem : UserControl
    {
        public SearchResultItem()
        {
            InitializeComponent();

            this.pibMain.Image = Properties.Resources.loading;
            this.Title = "";
            this.Year = "";
            this.Rating = "";
        }

        [Browsable(true)]
        public string Title
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }
        
        [Browsable(true)]
        public string Year
        {
            get { return this.lblYear.Text; }
            set { this.lblYear.Text = value; }
        }
        
        [Browsable(true)]
        public string Rating
        {
            get { return this.lblRating.Text; }
            set { this.lblRating.Text = value; }
        }

        public void SetImage(string url)
        {
            this.pibMain.Image = Properties.Resources.loading;

            if (string.IsNullOrWhiteSpace(url))
                return;
            this.pibMain.LoadAsync(url);
        }
    }
}
