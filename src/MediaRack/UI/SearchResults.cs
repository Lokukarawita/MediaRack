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
    public partial class SearchResults : UserControl
    {
        private List<SearchResultItem> items;

        public SearchResults()
        {
            InitializeComponent();
            items = new List<SearchResultItem>();
        }

        public void AddItem(string title, string year, string rating, string url)
        {
            var sri = new SearchResultItem();
            sri.Title = title;
            sri.Year = year;
            sri.Rating = rating;
            //sri.Dock = DockStyle.Fill;
            //this.items.Add(sri);

            flowLayoutPanel1.Controls.Add(sri);
            sri.SetImage(url);

        }
    }
}
