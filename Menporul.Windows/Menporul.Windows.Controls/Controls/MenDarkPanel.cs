using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Dark Panel
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenDarkPanel :Panel
    {
        /// <summary>
        /// 
        /// </summary>
        public MenDarkPanel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            this.BackColor = System.Drawing.Color.Black;
            base.OnPaintBackground(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            this.BackColor = System.Drawing.Color.Black;
            base.OnPaint(e);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Location.X <= this.Width && e.Location.X >= (this.Width - 50) && e.Location.Y >= 1 && e.Location.Y <= 20)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MenConstants.WM_NCHITTEST)
            {  // WM_NCHITTEST
                m.Result = new IntPtr(-1);
                return;
            }
            base.WndProc(ref m);
        }
    }
}
