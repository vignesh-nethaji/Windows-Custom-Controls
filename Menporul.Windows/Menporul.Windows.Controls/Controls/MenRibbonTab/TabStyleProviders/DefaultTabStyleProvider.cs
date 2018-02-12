using Menporul.Windows.Controls.Core;
using System.Drawing;
using Menporul.Windows.Controls;

namespace System.Windows.Forms
{
    /// <summary>
    /// Menporul Deafult Tab Style Provider
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    [System.ComponentModel.ToolboxItem(false)]
    public class DefaultTabStyleProvider : TabStyleProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabControl"></param>
        public DefaultTabStyleProvider(MenRibbonTab tabControl)
            : base(tabControl)
        {
            this._ImageAlign = ContentAlignment.MiddleRight;
            this._Overlap = 7;
            this.Padding = new Point(14, 1);
            this._TextColor = Color.White;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tabBounds"></param>
        public override void AddTabBorder(System.Drawing.Drawing2D.GraphicsPath path, System.Drawing.Rectangle tabBounds)
        {
            path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
            path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
            path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Brush GetTabBackgroundBrush(int index)
        {
            SolidBrush fillBrush = null;
            Color bgColor = Color.Transparent;

            if (this._TabControl.SelectedIndex == index)
            {
                bgColor = MenConstants.Thin;
            }
            else if (!this._TabControl.TabPages[index].Enabled)
            {
                bgColor = Color.Green;
            }
            else if (this.HotTrack && index == this._TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                bgColor = Color.FromArgb(108, 116, 118);
            }

            fillBrush = new SolidBrush(bgColor);

            return fillBrush;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override Brush GetPageBackgroundBrush(int index)
        {
            Color light = Color.Transparent;

            if (this._TabControl.SelectedIndex == index)
            {
                light = Color.Transparent;
            }
            else if (!this._TabControl.TabPages[index].Enabled)
            {
                light = Color.Transparent;
            }
            else if (this._HotTrack && index == this._TabControl.ActiveIndex)
            {
                //	Enable hot tracking
                light = Color.Transparent;
            }

            return new SolidBrush(light);
        }
    }
}
