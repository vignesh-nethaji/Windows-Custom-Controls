using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul TabControl
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenTabControl : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1300 + 40)
            {
                RECT rc = (RECT)m.GetLParam(typeof(RECT));
                rc.Left -= 4;
                rc.Right += 2;
                rc.Top -= 7;
                rc.Bottom += 0;
                Marshal.StructureToPtr(rc, m.LParam, true);
            }
            base.WndProc(ref m);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
    }
}
