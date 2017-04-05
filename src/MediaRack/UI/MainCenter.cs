using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace MediaRack.UI
{
    public class MainCenter : Panel
    {
        private Label label1;
    
        public MainCenter()
        {
            base.BackColor = Color.Transparent;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 27.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.ResumeLayout(false);

        }

        
        [DefaultValue("mv_title"), Browsable(true), EditorBrowsable()]
        public string MovieTitle 
        {
            get { return label1.Text; }
            set { label1.Text = value; Invalidate(); }
        }
    }
}
