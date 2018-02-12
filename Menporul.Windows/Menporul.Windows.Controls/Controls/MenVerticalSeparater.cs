using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Vertical Separator
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenVerticalSeparater : UserControl
    {
        /// <summary>
        /// Vertical Separator back ground Color
        /// </summary>
        private Pen _penBackGround = new Pen(Core.MenConstants.VerticalSeparator);
        /// <summary>
        /// Initialize Name and Size
        /// </summary>
        public MenVerticalSeparater()
        {
            this.SuspendLayout();
            this.Name = "CafeVerticalSeparator";
            this.Size = new System.Drawing.Size(1, 100);
            this.ResumeLayout(false);
        }
        /// <summary>
        /// Paint the custom background color and size 
        /// the Control draw Height is always minus 10 than default height
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(_penBackGround, 0, 10, 0, this.Height - 10);
        }
    }
}
