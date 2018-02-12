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
    ///  Menporul Progress Bar
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenProgressBar : ProgressBar
    {
        /// <summary>
        /// Progress Color 
        /// </summary>
        private SolidBrush _bshPercentage = new SolidBrush(MenConstants.PBPercentage);
        /// <summary>
        /// Progress Text Color
        /// </summary>
        private SolidBrush _bshText = new SolidBrush(MenConstants.PBText);
        /// <summary>
        /// Border color of Progress bar
        /// </summary>
        private Color _colorBorder = Core.MenConstants.PBBorder;
        /// <summary>
        /// Initialize and allow userpaint  to draw custom
        /// </summary>
        public MenProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        /// <summary>
        /// Draw custom progress bar 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Get the Entire Progress bar rectangle
            Rectangle rec = e.ClipRectangle;
            //Draw the progress bar border 
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, _colorBorder, ButtonBorderStyle.Solid);
            // increase the width based on the progress value for draw the progress
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            //Draw the progress
            e.Graphics.FillRectangle(_bshPercentage, 0, 0, rec.Width, rec.Height);
            //get the text size based on the Font
            var textSize = e.Graphics.MeasureString(this.Value + "%", this.Font);
            // Find the Text X position to draw on the progress bar
            var textX = this.Width / 2 - (textSize.Width / 2.0F);
            // Find the Text Y position to draw on the progress bar
            var textY = this.Height / 2 - (textSize.Height / 2.0F);
            //Draw the progress value 
            e.Graphics.DrawString(this.Value + "%", this.Font, _bshText, textX, textY);
        }
    }
}
