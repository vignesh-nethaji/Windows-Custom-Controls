using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul TextBox
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenTextBox : TextBox
    {
        /// <summary>
        /// 
        /// </summary>
        private Color _focusBorderColor = MenConstants.TxtFocusBorder;
        /// <summary>
        /// 
        /// </summary>
        private Color _defaultBorderColor = MenConstants.TxtDefaultBorder;
        /// <summary>
        /// 
        /// </summary>
        public MenTextBox()
        {


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == MenConstants.WM_NCPAINT || m.Msg == MenConstants.WM_PAINT)
            {
                Rectangle rect = new Rectangle(0, 0, Width, Height);
                if (this.Focused)
                {
                    
                    var dc = GetWindowDC(Handle);
                    using (Graphics g = Graphics.FromHdc(dc))
                    {
                        ControlPaint.DrawBorder(g, rect,
                       _focusBorderColor, 2, ButtonBorderStyle.Solid,
                       _focusBorderColor, 2, ButtonBorderStyle.Solid,
                       _focusBorderColor, 2, ButtonBorderStyle.Solid,
                       _focusBorderColor, 2, ButtonBorderStyle.Solid);
                    }
                }
                else
                {
                    
                    var dc = GetWindowDC(Handle);
                    using (Graphics g = Graphics.FromHdc(dc))
                    {
                        ControlPaint.DrawBorder(g, rect,
                       _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                       _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                       _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                       _defaultBorderColor, 1, ButtonBorderStyle.Solid);
                    }
                }
            }
           
        }
    }
}
