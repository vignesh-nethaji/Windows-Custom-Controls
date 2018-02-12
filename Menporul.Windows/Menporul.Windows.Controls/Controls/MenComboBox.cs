using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul ComboBox
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenComboBox : ComboBox
    {
        /// <summary>
        /// 
        /// </summary>
        private Image _image = global::Menporul.Windows.Controls.MenResource.Expand;
        /// <summary>
        /// 
        /// </summary>
        private Color _defaultBorderColor = MenConstants.CbxDefaultBorder;
        /// <summary>
        /// 
        /// </summary>
        private Color _focusBorderColor = MenConstants.CbxFocusBorder;
        /// <summary>
        /// 
        /// </summary>
        private Color _highLightColor = MenConstants.CbxHighLight;
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush bshArrowBackgroundDefaultColor = new SolidBrush(MenConstants.Light);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush bshArrowBackgroundDisabledColor = new SolidBrush(MenConstants.Thin);
        /// <summary>
        /// 
        /// </summary>
        public MenComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.IntegralHeight = true;
            this.DropDownHeight = 500;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //this.BackColor = Color.Red;
            if (!DesignMode)
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(_highLightColor), e.Bounds);
                    e.Graphics.DrawString(this.Items[e.Index].ToString(),
                                              e.Font,
                                              new SolidBrush(Color.White),
                                              new Point(e.Bounds.X, e.Bounds.Y));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                    if (e.Index != -1)
                    {
                        e.Graphics.DrawString(this.Items[e.Index].ToString(),
                                              e.Font,
                                              new SolidBrush(Color.Black),
                                              new Point(e.Bounds.X, e.Bounds.Y));
                    }
                    
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
       
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateSolidBrush(int color);
        private const int WM_PAINT = 0x000F;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);

                if (this.Enabled)
                {
                    Rectangle rect = new Rectangle(this.Width - 17, 1, 16, this.Height - 2);
                    //arrow background Color Redraw
                    g.FillRectangle(bshArrowBackgroundDefaultColor, rect);
                    //arrow Draw
                    g.DrawImage(_image, rect.X + 3, rect.Y + 6, 8, 8);
                }
                else
                {
                    Rectangle rect = new Rectangle(2, 1, this.Width - 2, this.Height - 2);
                    Rectangle arrowRect = new Rectangle(this.Width - 17, 1, 16, this.Height - 2);
                    //arrow background Color Redraw
                    g.FillRectangle(bshArrowBackgroundDisabledColor, rect);
                    //arrow Draw
                    g.DrawImage(ToolStripRenderer.CreateDisabledImage(_image), arrowRect.X + 3, arrowRect.Y + 6, 8, 8);

                    IntPtr brush;
                    brush = CreateSolidBrush(ColorTranslator.ToWin32(MenConstants.Thin));
                    m.Result = brush;
                }
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                if (this.Focused)
                {
                    ControlPaint.DrawBorder(g, bounds,
                    _focusBorderColor, 2, ButtonBorderStyle.Solid,
                    _focusBorderColor, 2, ButtonBorderStyle.Solid,
                    _focusBorderColor, 2, ButtonBorderStyle.Solid,
                    _focusBorderColor, 2, ButtonBorderStyle.Solid);
                }
                else
                {
                    ControlPaint.DrawBorder(g, bounds,
                     _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                     _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                     _defaultBorderColor, 1, ButtonBorderStyle.Solid,
                     _defaultBorderColor, 1, ButtonBorderStyle.Solid);
                }

            }
            
        }
    }
}
