using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul TransparentLabel
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenTransparentLabel : Label
    {
        /// <summary>
        /// 
        /// </summary>
        public MenTransparentLabel()
        {

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
