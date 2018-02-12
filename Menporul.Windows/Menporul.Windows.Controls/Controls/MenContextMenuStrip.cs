using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;
namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul MenContextMenuStrip
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenContextMenuStrip : ContextMenuStrip
    {
        public MenContextMenuStrip()
        {
            this.RenderMode = ToolStripRenderMode.Professional;
            this.Renderer = new MenMenuStripColorRenderer();
        }
    }
}
