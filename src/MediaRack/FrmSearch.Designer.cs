namespace MediaRack
{
    partial class FrmSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearch));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.listbx_search = new System.Windows.Forms.ListBox();
            this.btn_pic_srch = new System.Windows.Forms.PictureBox();
            this.searchResults1 = new MediaRack.UI.SearchResults();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_srch)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(28, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Movie Name";
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(133, 54);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(236, 23);
            this.txt_search.TabIndex = 1;
            // 
            // listbx_search
            // 
            this.listbx_search.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listbx_search.FormattingEnabled = true;
            this.listbx_search.ItemHeight = 15;
            this.listbx_search.Location = new System.Drawing.Point(13, 99);
            this.listbx_search.Name = "listbx_search";
            this.listbx_search.Size = new System.Drawing.Size(464, 64);
            this.listbx_search.TabIndex = 3;
            this.listbx_search.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listbx_search_MouseClick);
            this.listbx_search.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listbx_search_MouseDoubleClick);
            // 
            // btn_pic_srch
            // 
            this.btn_pic_srch.BackgroundImage = global::MediaRack.Properties.Resources.bk_img;
            this.btn_pic_srch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pic_srch.Image = ((System.Drawing.Image)(resources.GetObject("btn_pic_srch.Image")));
            this.btn_pic_srch.Location = new System.Drawing.Point(375, 47);
            this.btn_pic_srch.Name = "btn_pic_srch";
            this.btn_pic_srch.Size = new System.Drawing.Size(91, 36);
            this.btn_pic_srch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_pic_srch.TabIndex = 4;
            this.btn_pic_srch.TabStop = false;
            this.btn_pic_srch.Click += new System.EventHandler(this.btn_pic_srch_Click);
            // 
            // searchResults1
            // 
            this.searchResults1.BackgroundImage = global::MediaRack.Properties.Resources.bk_img;
            this.searchResults1.Location = new System.Drawing.Point(13, 169);
            this.searchResults1.Name = "searchResults1";
            this.searchResults1.Size = new System.Drawing.Size(464, 293);
            this.searchResults1.TabIndex = 5;
            // 
            // FrmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MediaRack.Properties.Resources.bk_img;
            this.ClientSize = new System.Drawing.Size(489, 474);
            this.Controls.Add(this.searchResults1);
            this.Controls.Add(this.btn_pic_srch);
            this.Controls.Add(this.listbx_search);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moviez - Search";
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_srch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.ListBox listbx_search;
        private System.Windows.Forms.PictureBox btn_pic_srch;
        private UI.SearchResults searchResults1;
    }
}

