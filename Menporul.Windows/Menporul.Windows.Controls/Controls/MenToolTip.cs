using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul ToolTip
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenToolTip : ToolTip
    {
        /// <summary>
        /// it is used to define the string position and alignment on ToolTip
        /// </summary>
        private StringFormat _strFormat;
        /// <summary>
        /// set Primary Font has ToolTip Font
        /// </summary>
        private Font _font = Core.MenConstants.AppPrimaryFont;
        /// <summary>
        /// set Background color of the ToolTip
        /// </summary>
        private Brush _bshBackGround = new SolidBrush(Core.MenConstants.ToolTipBackGround);
        /// <summary>
        /// This Brush is used to color the border of the ToolTip
        /// </summary>
        private Pen _penBorder = new Pen(Core.MenConstants.ToolTipBorder);
        /// <summary>
        /// it is used to set the Text color on ToolTip
        /// </summary>
        private Brush _bshText = new SolidBrush(Core.MenConstants.ToolTipText);
        /// <summary>
        /// Intialize Default values 
        /// <property name="OwnerDraw"> Enable Tooltip to draw has customized</property>
        /// <property name="AutoPopDelay"> set Time for Tooltip Visible</property>
        /// <property name="InitialDelay">Set the Milliseconds for showing the tooltip after mouse over</property>
        /// <property name="ReshowDelay">Set milliseconds for ReshowDelay</property>
        /// <Event name="Draw"> Mapp the Event for Custom Drawing the ToolTip</Event>
        /// </summary>
        public MenToolTip()
        {            
            //ToolTip Properties initialization
            this.OwnerDraw = true;
            this.AutoPopDelay = 5000;
            this.InitialDelay = 500;
            this.ReshowDelay = 10;
            this.Draw += new DrawToolTipEventHandler(this.DrawToolTip);

            //string Format Properites Initialization
            _strFormat = new StringFormat();
            _strFormat.Alignment = StringAlignment.Center;
            _strFormat.LineAlignment = StringAlignment.Center;
            _strFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            _strFormat.FormatFlags = StringFormatFlags.NoWrap;
        }
        /// <summary>
        /// Draw customized Tooltip
        /// </summary>
        /// <param name="sender">Current Object</param>
        /// <param name="e">Draw Tooltip Event handler</param>
        private void DrawToolTip(object sender, DrawToolTipEventArgs e)
        {
            // Draw the custom background.
            e.Graphics.FillRectangle(_bshBackGround, e.Bounds);

            // Draw the custom border to appear 3-dimensional.
            //Draw Left Line And Top Line
            e.Graphics.DrawLines(_penBorder, new Point[] {
                    new Point (0, e.Bounds.Height - 1),
                    new Point (0, 0),
                    new Point (e.Bounds.Width - 1, 0)
                });

            //Draw Bottom Line and Right Line
            e.Graphics.DrawLines(_penBorder, new Point[] {
                    new Point (0, e.Bounds.Height - 1),
                    new Point (e.Bounds.Width - 1, e.Bounds.Height - 1),
                    new Point (e.Bounds.Width - 1, 0)
                });

            // Draw the custom text.
            e.Graphics.DrawString(e.ToolTipText, _font, _bshText, e.Bounds, _strFormat);
        }
    }
}
