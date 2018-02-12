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
    /// Men FormBorder Splitter
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenFormBorderSplitter : Splitter
    {
        /// <summary>
        /// 
        /// </summary>
        public MenFormBorderSplitter()
        {
            this.BackColor = MenConstants.Dark;
            this.Size = new Size(3, 3); ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MenConstants.WM_NCHITTEST)
            {  
                m.Result = new IntPtr(-1);
                return;
            }
            base.WndProc(ref m);
        }
    }
}
