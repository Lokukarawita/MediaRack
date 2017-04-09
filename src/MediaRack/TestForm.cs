using MediaRack.Core.Data.Common;
using MediaRack.Core.Data.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaRack
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //MediaRackEntities ent = new MediaRackEntities();
           // //Core.Data.Local.MediaRackEntities

           //ent.Racks.Add(new Rack()
           //{
           //    Classification = MediaClassification.Movie,
           //    Timestamp = DateTime.Now,
           //    SyncStatus = LocalSyncStatus.NEW,
           //    Comment = "First try",
           //    Grade = "A",
           //    Watched = true,
           //    WatchedOn = DateTime.Now,
           //    CompositionInfo = "{}",
           //    FileInfo = "{}",
           //    IDInfo = "{}"
           //});
           //ent.SaveChanges();
        }
    }
}
