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
    /// Menporul Horizontal Separator
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenHorizontalSeparater : UserControl
    {
        /// <summary>
        /// Pen for Background Color of this control
        /// </summary>
        private Pen _penBackGround = new Pen(MenConstants.HorizontalSeparator);
        /// <summary>
        /// Initialize default height and Width of Menporul Horizontal Separator
        /// </summary>
        public MenHorizontalSeparater()
        {
            this.Width = 200;
            this.Height = 1;
        }
        /// <summary>
        /// Paint Event is redraw the Control with Constant Height and Dynamic Width.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawLine(_penBackGround, 0, 0, this.Width, 0);
            g.DrawLine(_penBackGround, 0, 1, this.Width, 1);
        }
    }
}
