using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using WatTmdb.V3;

namespace MediaRack
{
    public partial class Form3 : Form
    {
        delegate void SetPlotTextCallback(string text);   
        DataSet ds;
        string mv_id;
        int a=0,b=0,c=0;
       
        public Form3()
        {
            InitializeComponent();

        }
               
        private void Form3_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Load_All_Movies("");

            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }
       
        //Exit Main Form (Form3)
        private void btn_pic_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Add New Movie to Database
        private void btn_pic_add_Click(object sender, EventArgs e)
        {
            DialogResult rslt;
            Form1 frm1 = new Form1();
            rslt = frm1.ShowDialog(this);

            TmdbMovie selectedmv = null;
            if (rslt == System.Windows.Forms.DialogResult.OK)
            {
                selectedmv = frm1.SelectedMovie;
                frm1.Dispose();

                Form2 vieMv = new Form2(selectedmv);
                rslt = vieMv.ShowDialog(this);
                if (rslt == System.Windows.Forms.DialogResult.OK)
                {
                    Load_All_Movies("");
                }
            }
           
        }
        //Add Newly Added Movie Details to Listview
        private void btn_pic_refresh_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Load_All_Movies("");
        }
        //Delete Data from Listview and Database
        private void btn_pic_delete_Click(object sender, EventArgs e)
        {
            DialogResult rslt;

            if (listView1.SelectedItems.Count > 0)
            {
                rslt = MessageBox.Show("Delete this movie?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (rslt == System.Windows.Forms.DialogResult.Yes)
                {


                    int idx = listView1.SelectedIndices[0];
                    listView1.Items[idx].Remove();

                    ds.Tables[0].Rows.RemoveAt(idx);

                    OleDbCommand cmd = new OleDbCommand(("DELETE FROM mv_det WHERE mv_id=@1"), DBConnection.Open());
                    cmd.Parameters.AddWithValue("@1", mv_id);
                    cmd.ExecuteNonQuery();
                    DBConnection.Close();

                    if (listView1.Items.Count > 0)
                    {
                        idx = (idx == listView1.Items.Count ? 0 : idx);
                        listView1.Items[idx].Selected = true;
                    }
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Load_All_Movies(txt_search.Text);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                SetFields();
            }
             
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
            if (listView1.Columns[e.Column].Text=="Movie Name")
            {
                
                if (a == 1)
                {
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_name DESC");
                    a = 0;
                }
                else
                {
                    a = a + 1;
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_name");
                }
                

            }
            else if (listView1.Columns[e.Column].Text=="Vote")
            {
                if (b==1)
                {
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_avgvote DESC  ");
                    b = 0;
                }
                else
                {
                    b = b + 1;
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_avgvote  ");
                } 


            }
            else if (listView1.Columns[e.Column].Text=="DVD No")
            {
                if (c==1)
                {
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_dvdno DESC ");
                    c = 0;
                }
                else
                {
                    c = c + 1;
                    listviewdet("SELECT * FROM mv_det ORDER BY mv_dvdno  ");
                }
               

            }

        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int a = (int)e.Argument;
                pictureBox1.Image = MovieFiles.GetCoverImage(a);

                backgroundWorker2.RunWorkerAsync(a);


                string t = MovieFiles.ReadPlot(a);
                SetPlotText(t);
            }
            catch (Exception)
            {
   
            }   
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            this.BackgroundImage = MovieFiles.GetBackImage((int)e.Argument);
        }

        private void SetFields()
        { 
            try
            {
                    if (backgroundWorker1.IsBusy)
                        backgroundWorker1.CancelAsync();

                    ListViewItem itm = listView1.SelectedItems[0];
                    int idx = listView1.SelectedIndices[0];

                    mv_id = ds.Tables[0].Rows[idx]["mv_id"].ToString();
                    lbl_mvname.Text = ds.Tables[0].Rows[idx]["mv_name"].ToString();
                    lbl_runtime.Text = ds.Tables[0].Rows[idx]["mv_runtime"].ToString();
                    txt_cast.Text = ds.Tables[0].Rows[idx]["mv_act"].ToString();
                    lbl_genre.Text = ds.Tables[0].Rows[idx]["mv_genre"].ToString();

                    DateTime rdate = DateTime.Parse(ds.Tables[0].Rows[idx]["mv_rdte"].ToString());
                    lbl_rlesdte.Text = rdate.ToString("MM/dd/yyyy");

                    int a = Convert.ToInt32(mv_id);
                    backgroundWorker1.RunWorkerAsync(a);
                    
                
            }
            catch (Exception)
            {
                  
            }
        }
        //Load Movie details to Database
        private void Load_All_Movies(string sby)
        {

            try
            {
                OleDbDataAdapter cm = new OleDbDataAdapter("SELECT * FROM mv_det WHERE mv_name LIKE @1 OR mv_dvdno LIKE @2  ORDER BY mv_name", DBConnection.Open());
                cm.SelectCommand.Parameters.AddWithValue("@1", string.Format("%{0}%", sby));
                cm.SelectCommand.Parameters.AddWithValue("@2", string.Format("%{0}%", sby));
                ds = new DataSet();
                cm.Fill(ds);

                listView1.Items.Clear();
                listView1.Refresh();

                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem itm = listView1.Items.Add(dt.Rows[i]["mv_name"].ToString());
                    itm.SubItems.Add(dt.Rows[i]["mv_avgvote"].ToString());
                    itm.SubItems.Add(dt.Rows[i]["mv_dvdno"].ToString());
                    
                }

                DBConnection.Close();
            }
            catch (Exception)
            {
                
              
            }
   
        }
        private void listviewdet(string state)
        {

            OleDbDataAdapter cm = new OleDbDataAdapter(state, DBConnection.Open());
             ds = new DataSet();
            cm.Fill(ds);

            listView1.Items.Clear();
            listView1.Refresh();

            DataTable dt = ds.Tables[0];


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem itm = listView1.Items.Add(dt.Rows[i]["mv_name"].ToString());
                itm.SubItems.Add(dt.Rows[i]["mv_avgvote"].ToString());
                itm.SubItems.Add(dt.Rows[i]["mv_dvdno"].ToString());

            }

            DBConnection.Close();
 
        }

        /// <summary>
        /// Set plot on the txt_plot textbox (used to vector a thread call back into UI thread)
        /// </summary>
        /// <param name="text">Text to be set on txt_plot text</param>
        private void SetPlotText(string text)
        {
            SetPlotTextCallback cb = new SetPlotTextCallback(SetPlotText);
         
            if (txt_plot.InvokeRequired)
            {
                Invoke(cb, text);
            }
            else
            {
                txt_plot.Text = text;
                
            }
        }

        private void txt_plot_TextChanged(object sender, EventArgs e)
        {
           // string plt = txt_plot.Text;

            if (txt_plot.Text.Length > 516)
                {
                    txt_plot.ScrollBars = ScrollBars.Vertical;
                }
                else
                {
                    txt_plot.ScrollBars = ScrollBars.None;
                }

           // txt_plot.Text = plt;
        }
    }
}
