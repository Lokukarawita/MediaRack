using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace mv_collect_db
{
    public class TitleLable : Label
    {
        private Image bgimg = null;
        
        public TitleLable()
        {
            base.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bgimg != null)
            {
                e.Graphics.DrawImage(bgimg, 0, 0, this.Width + 15, this.Height);
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Background image for the label.
        /// </summary>
        [DefaultValue(null), BrowsableAttribute(true)]
        public new Image Image 
        {
            get { return bgimg; }
            set { bgimg = value; Invalidate(); }
        }
    }
}
