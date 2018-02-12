using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Menporul.Windows.Controls.Core
{
    /// <summary>
    /// Menporul MenuStripColorRenderer
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenMenuStripColorRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        private Image _arrowImage = global::Menporul.Windows.Controls.MenResource.Forward;
        /// <summary>
        /// 
        /// </summary>
        public MenMenuStripColorRenderer()
            : base(new MenuStripColors())
        {
            this.RoundedEdges = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            e.ToolStrip.BackColor = MenConstants.ContextMenuBackGround;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);
            e.TextColor = MenConstants.ContextMenuText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Rectangle rect = e.ArrowRectangle;
            rect.Y += 6;
            rect.Height = 8;
            rect.Width = 8;
            if (e.Item.Enabled)
            {
                e.Graphics.DrawImage(_arrowImage, rect);
            }
            else
            {
                e.Graphics.DrawImage(ToolStripRenderer.CreateDisabledImage(_arrowImage), rect);
            }
        }
    }
}
