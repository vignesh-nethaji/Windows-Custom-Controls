using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menporul.Windows.Controls.Core
{
    /// <summary>
    /// Menporul ToolStripProfessionalRenderer
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenToolStripColorRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        private Image _arrowImage = global::Menporul.Windows.Controls.MenResource.Forward;
        /// <summary>
        /// 
        /// </summary>
        public MenToolStripColorRenderer()
            : base(new MenToolStripColors())
        {
            this.RoundedEdges = false;
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
