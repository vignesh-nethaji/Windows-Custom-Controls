using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;
using System.Drawing;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul ToolStrip
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenToolStrip : ToolStrip
    {
        /// <summary>
        /// 
        /// </summary>
        private ToolStripItem mouseOverItem = null;
        /// <summary>
        /// 
        /// </summary>
        private Point mouseOverPoint;
        /// <summary>
        /// 
        /// </summary>
        private Timer timer;
        /// <summary>
        /// 
        /// </summary>
        public MenToolTip Tooltip;
        /// <summary>
        /// 
        /// </summary>
        public int ToolTipInterval = 4000;
        /// <summary>
        /// 
        /// </summary>
        public string ToolTipText;
        /// <summary>
        /// 
        /// </summary>
        public bool ToolTipShowUp;

        /// <summary>
        /// 
        /// </summary>
        public MenToolStrip()
            : base()
        {
            this.BackColor = MenConstants.Thin;
            this.Renderer = new Menporul.Windows.Controls.Core.MenToolStripColorRenderer();

            //Tooltips initialization
            ShowItemToolTips = false;
            timer = new Timer();
            timer.Enabled = false;
            timer.Interval = SystemInformation.MouseHoverTime;
            timer.Tick += new EventHandler(timer_Tick);
            Tooltip = new MenToolTip();
        }
        /// <summary>
        /// Custom Drawn Toolstrip
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var item in this.Items)
            {
                var cbx = item as ToolStripComboBox;
                if (cbx != null)
                {
                    var location = cbx.ComboBox.Location;
                    var size = cbx.ComboBox.Size;
                    Pen cbBorderPen = new Pen(MenConstants.LightGrey);
                    Rectangle rect = new Rectangle(
                            location.X - 1,
                            location.Y - 1,
                            size.Width + 1,
                            size.Height + 1);

                    e.Graphics.DrawRectangle(cbBorderPen, rect);
                }
            }
        }
        /// <summary>
        /// Show Tooltip
        /// </summary>
        /// <param name="mea"></param>
        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
            ToolStripItem newMouseOverItem = this.GetItemAt(mea.Location);
            if (mouseOverItem != newMouseOverItem ||
                (Math.Abs(mouseOverPoint.X - mea.X) > SystemInformation.MouseHoverSize.Width || (Math.Abs(mouseOverPoint.Y - mea.Y) > SystemInformation.MouseHoverSize.Height)))
            {
                mouseOverItem = newMouseOverItem;
                mouseOverPoint = mea.Location;
                if (Tooltip != null)
                    Tooltip.Hide(this);
                timer.Stop();
                timer.Start();
            }
        }
        /// <summary>
        /// hide Tooltip on Clikc
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ToolStripItem newMouseOverItem = this.GetItemAt(e.Location);
            if (newMouseOverItem != null && Tooltip != null)
            {
                Tooltip.Hide(this);
            }
        }
        /// <summary>
        ///  Get Current mouse over Control based on Location
        /// </summary>
        /// <param name="mea"></param>
        protected override void OnMouseUp(MouseEventArgs mea)
        {
            base.OnMouseUp(mea);
            ToolStripItem newMouseOverItem = this.GetItemAt(mea.Location);
        }
        /// <summary>
        /// hide tooltip
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            timer.Stop();
            if (Tooltip != null)
                Tooltip.Hide(this);
            mouseOverPoint = new Point(-50, -50);
            mouseOverItem = null;
        }
        /// <summary>
        /// tooltip show based on the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            try
            {
                Point currentMouseOverPoint;
                if (ToolTipShowUp)
                    currentMouseOverPoint = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y - Cursor.Current.Size.Height + Cursor.Current.HotSpot.Y));
                else
                    currentMouseOverPoint = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y + Cursor.Current.Size.Height - Cursor.Current.HotSpot.Y));

                if (mouseOverItem == null)
                {
                    if (ToolTipText != null && ToolTipText.Length > 0)
                    {
                        if (Tooltip == null)
                            Tooltip = new MenToolTip();
                        Tooltip.Show(ToolTipText, this, currentMouseOverPoint, ToolTipInterval);
                    }
                }
                else if ((!(mouseOverItem is ToolStripDropDownButton) && !(mouseOverItem is ToolStripSplitButton)) ||
                    ((mouseOverItem is ToolStripDropDownButton) && !((ToolStripDropDownButton)mouseOverItem).DropDown.Visible) ||
                    (((mouseOverItem is ToolStripSplitButton) && !((ToolStripSplitButton)mouseOverItem).DropDown.Visible)))
                {
                    if (mouseOverItem.ToolTipText != null && mouseOverItem.ToolTipText.Length > 0 && Tooltip != null)
                    {
                        if (Tooltip == null)
                            Tooltip = new MenToolTip();
                        Tooltip.Show(mouseOverItem.ToolTipText, this, currentMouseOverPoint, ToolTipInterval);
                    }
                }
            }
            catch
            { }
        }
        /// <summary>
        /// dispose timer and tooltip
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                timer.Dispose();
                Tooltip.Dispose();
            }
        }
    }
}
