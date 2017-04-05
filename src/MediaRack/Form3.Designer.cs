namespace mv_collect_db
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.listView1 = new System.Windows.Forms.ListView();
            this.movie_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dvd_no = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_mvcast = new System.Windows.Forms.Label();
            this.lbl_rlesdte = new System.Windows.Forms.Label();
            this.txt_plot = new System.Windows.Forms.TextBox();
            this.lbl_runtime = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_cast = new System.Windows.Forms.TextBox();
            this.lbl_genre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_pic_delete = new System.Windows.Forms.PictureBox();
            this.btn_pic_refresh = new System.Windows.Forms.PictureBox();
            this.btn_pic_add = new System.Windows.Forms.PictureBox();
            this.btn_pic_exit = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.back_pic = new System.Windows.Forms.PictureBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl_mvname = new mv_collect_db.TitleLable();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.back_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.movie_name,
            this.vote,
            this.dvd_no});
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(322, 145);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(349, 388);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // movie_name
            // 
            this.movie_name.Text = "Movie Name";
            this.movie_name.Width = 208;
            // 
            // vote
            // 
            this.vote.Text = "Vote";
            this.vote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.vote.Width = 49;
            // 
            // dvd_no
            // 
            this.dvd_no.Text = "DVD No";
            this.dvd_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dvd_no.Width = 73;
            // 
            // lbl_mvcast
            // 
            this.lbl_mvcast.AutoSize = true;
            this.lbl_mvcast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mvcast.Location = new System.Drawing.Point(12, 343);
            this.lbl_mvcast.Name = "lbl_mvcast";
            this.lbl_mvcast.Size = new System.Drawing.Size(0, 15);
            this.lbl_mvcast.TabIndex = 7;
            // 
            // lbl_rlesdte
            // 
            this.lbl_rlesdte.AutoSize = true;
            this.lbl_rlesdte.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rlesdte.Location = new System.Drawing.Point(165, 279);
            this.lbl_rlesdte.Name = "lbl_rlesdte";
            this.lbl_rlesdte.Size = new System.Drawing.Size(71, 20);
            this.lbl_rlesdte.TabIndex = 9;
            this.lbl_rlesdte.Text = "lbl_rlesdte";
            // 
            // txt_plot
            // 
            this.txt_plot.BackColor = System.Drawing.SystemColors.Control;
            this.txt_plot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_plot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_plot.Location = new System.Drawing.Point(3, 57);
            this.txt_plot.Multiline = true;
            this.txt_plot.Name = "txt_plot";
            this.txt_plot.ReadOnly = true;
            this.txt_plot.Size = new System.Drawing.Size(284, 205);
            this.txt_plot.TabIndex = 10;
            this.txt_plot.TextChanged += new System.EventHandler(this.txt_plot_TextChanged);
            // 
            // lbl_runtime
            // 
            this.lbl_runtime.AutoSize = true;
            this.lbl_runtime.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_runtime.Location = new System.Drawing.Point(6, 265);
            this.lbl_runtime.Name = "lbl_runtime";
            this.lbl_runtime.Size = new System.Drawing.Size(153, 40);
            this.lbl_runtime.TabIndex = 15;
            this.lbl_runtime.Text = "lbl_runtime";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_cast);
            this.panel1.Controls.Add(this.lbl_genre);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_mvcast);
            this.panel1.Controls.Add(this.lbl_runtime);
            this.panel1.Controls.Add(this.txt_plot);
            this.panel1.Controls.Add(this.lbl_rlesdte);
            this.panel1.Location = new System.Drawing.Point(677, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 388);
            this.panel1.TabIndex = 16;
            // 
            // txt_cast
            // 
            this.txt_cast.BackColor = System.Drawing.SystemColors.Control;
            this.txt_cast.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_cast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cast.Location = new System.Drawing.Point(3, 338);
            this.txt_cast.Multiline = true;
            this.txt_cast.Name = "txt_cast";
            this.txt_cast.Size = new System.Drawing.Size(284, 37);
            this.txt_cast.TabIndex = 18;
            // 
            // lbl_genre
            // 
            this.lbl_genre.AutoSize = true;
            this.lbl_genre.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_genre.Location = new System.Drawing.Point(9, 309);
            this.lbl_genre.Name = "lbl_genre";
            this.lbl_genre.Size = new System.Drawing.Size(66, 20);
            this.lbl_genre.TabIndex = 17;
            this.lbl_genre.Text = "lbl_genre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 37);
            this.label2.TabIndex = 16;
            this.label2.Text = "Plot";
            // 
            // btn_pic_delete
            // 
            this.btn_pic_delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_pic_delete.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.btn_pic_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pic_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pic_delete.Image = global::mv_collect_db.Properties.Resources.btn_dele;
            this.btn_pic_delete.Location = new System.Drawing.Point(595, 570);
            this.btn_pic_delete.Name = "btn_pic_delete";
            this.btn_pic_delete.Size = new System.Drawing.Size(90, 26);
            this.btn_pic_delete.TabIndex = 20;
            this.btn_pic_delete.TabStop = false;
            this.btn_pic_delete.Click += new System.EventHandler(this.btn_pic_delete_Click);
            // 
            // btn_pic_refresh
            // 
            this.btn_pic_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_pic_refresh.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.btn_pic_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pic_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pic_refresh.Image = global::mv_collect_db.Properties.Resources.btn_refresh;
            this.btn_pic_refresh.Location = new System.Drawing.Point(697, 570);
            this.btn_pic_refresh.Name = "btn_pic_refresh";
            this.btn_pic_refresh.Size = new System.Drawing.Size(90, 26);
            this.btn_pic_refresh.TabIndex = 19;
            this.btn_pic_refresh.TabStop = false;
            this.btn_pic_refresh.Click += new System.EventHandler(this.btn_pic_refresh_Click);
            // 
            // btn_pic_add
            // 
            this.btn_pic_add.BackColor = System.Drawing.Color.Transparent;
            this.btn_pic_add.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.btn_pic_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pic_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pic_add.Image = global::mv_collect_db.Properties.Resources.btn_add;
            this.btn_pic_add.Location = new System.Drawing.Point(808, 570);
            this.btn_pic_add.Name = "btn_pic_add";
            this.btn_pic_add.Size = new System.Drawing.Size(73, 26);
            this.btn_pic_add.TabIndex = 18;
            this.btn_pic_add.TabStop = false;
            this.btn_pic_add.Click += new System.EventHandler(this.btn_pic_add_Click);
            // 
            // btn_pic_exit
            // 
            this.btn_pic_exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_pic_exit.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.btn_pic_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pic_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pic_exit.Image = global::mv_collect_db.Properties.Resources.btn_exit;
            this.btn_pic_exit.Location = new System.Drawing.Point(901, 570);
            this.btn_pic_exit.Name = "btn_pic_exit";
            this.btn_pic_exit.Size = new System.Drawing.Size(68, 26);
            this.btn_pic_exit.TabIndex = 17;
            this.btn_pic_exit.TabStop = false;
            this.btn_pic_exit.Click += new System.EventHandler(this.btn_pic_exit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(57, 145);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 388);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // back_pic
            // 
            this.back_pic.BackColor = System.Drawing.Color.Transparent;
            this.back_pic.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.back_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.back_pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.back_pic.Location = new System.Drawing.Point(24, 75);
            this.back_pic.Name = "back_pic";
            this.back_pic.Size = new System.Drawing.Size(974, 539);
            this.back_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.back_pic.TabIndex = 21;
            this.back_pic.TabStop = false;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(146, 573);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(170, 23);
            this.txt_search.TabIndex = 25;
            this.txt_search.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::mv_collect_db.Properties.Resources.bk_img;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(57, 570);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(83, 26);
            this.pictureBox2.TabIndex = 26;
            this.pictureBox2.TabStop = false;
            // 
            // lbl_mvname
            // 
            this.lbl_mvname.AutoSize = true;
            this.lbl_mvname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mvname.Font = new System.Drawing.Font("Segoe UI Light", 27.75F);
            this.lbl_mvname.ForeColor = System.Drawing.Color.White;
            this.lbl_mvname.Image = global::mv_collect_db.Properties.Resources.bk_img;
            this.lbl_mvname.Location = new System.Drawing.Point(48, 89);
            this.lbl_mvname.Name = "lbl_mvname";
            this.lbl_mvname.Size = new System.Drawing.Size(208, 50);
            this.lbl_mvname.TabIndex = 24;
            this.lbl_mvname.Text = "lbl_mvname";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1022, 626);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.lbl_mvname);
            this.Controls.Add(this.btn_pic_delete);
            this.Controls.Add(this.btn_pic_refresh);
            this.Controls.Add(this.btn_pic_add);
            this.Controls.Add(this.btn_pic_exit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.back_pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moviez";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pic_exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.back_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader movie_name;
        private System.Windows.Forms.ColumnHeader vote;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_mvcast;
        private System.Windows.Forms.Label lbl_rlesdte;
        private System.Windows.Forms.TextBox txt_plot;
        private System.Windows.Forms.Label lbl_runtime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_cast;
        private System.Windows.Forms.Label lbl_genre;
        private System.Windows.Forms.PictureBox btn_pic_exit;
        private System.Windows.Forms.PictureBox btn_pic_add;
        private System.Windows.Forms.PictureBox btn_pic_refresh;
        private System.Windows.Forms.PictureBox btn_pic_delete;
        private System.Windows.Forms.PictureBox back_pic;
        private TitleLable lbl_mvname;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.ColumnHeader dvd_no;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}