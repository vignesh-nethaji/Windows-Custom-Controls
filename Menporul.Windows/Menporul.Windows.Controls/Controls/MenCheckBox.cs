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
    /// Menporul CheckBox
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenCheckBox : CheckBox
    {
        /// <summary>
        /// 
        /// </summary>
        private Image _imgCheckMark = global::Menporul.Windows.Controls.MenResource.tick;
        /// <summary>
        /// 
        /// </summary>
        private Pen _penDefaultBorder = new Pen(MenConstants.ChkDefaultBorder);
        /// <summary>
        /// 
        /// </summary>
        private Pen _penDisabledBorder = new Pen(MenConstants.ChkDisabledBorder);
        /// <summary>
        /// 
        /// </summary>
        private Pen _penHoverBorder = new Pen(MenConstants.ChkHoverBorder);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshDefaultBackGround = new SolidBrush(MenConstants.ChkDefaultBackGround);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshDisabledBackGround = new SolidBrush(MenConstants.ChkDisabledBackGround);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshDefaultText = new SolidBrush(MenConstants.ChkDefaultText);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshDisabledText = new SolidBrush(MenConstants.ChkDisabledText);
        /// <summary>
        /// 
        /// </summary>
        private bool _isHover;
        /// <summary>
        /// 
        /// </summary>
        public MenCheckBox()
        {
            Appearance = System.Windows.Forms.Appearance.Button;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            FlatAppearance.BorderSize = 0;
            Height = 16;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //remove backcolor 
            e.Graphics.Clear(BackColor);

            //draw checkbox string
            e.Graphics.DrawString(Text, Font, Enabled ? _bshDefaultText : _bshDisabledText, 24, this.Height > 16 ? 4 : 0);
            if (_isHover)
            {
                HoverPaint(e);
            }
            else
            {
                NonHoverPaint(e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void HoverPaint(PaintEventArgs e)
        {
            Point pt;
            Rectangle rect;

            pt = new Point(2, 2);
            rect = new Rectangle(pt, new Size(16, 16));
            e.Graphics.FillRectangle(Enabled ? _bshDefaultBackGround : _bshDisabledBackGround, rect);
            if (Checked)
            {
                e.Graphics.DrawImage(Enabled ? _imgCheckMark : ToolStripRenderer.CreateDisabledImage(_imgCheckMark), rect);
            }
            e.Graphics.DrawRectangle(Enabled ? _penHoverBorder : _penDisabledBorder, rect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void NonHoverPaint(PaintEventArgs e)
        {
            Point pt;
            Rectangle rect;
            pt = new Point(3, 3);
            rect = new Rectangle(pt, new Size(14, 14));

            e.Graphics.FillRectangle(Enabled ? _bshDefaultBackGround : _bshDisabledBackGround, rect);
            if (Checked)
            {
                e.Graphics.DrawImage(Enabled ? _imgCheckMark : ToolStripRenderer.CreateDisabledImage(_imgCheckMark), rect);
            }
            e.Graphics.DrawRectangle(Enabled ? _penDefaultBorder : _penDisabledBorder, rect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _isHover = !_isHover;
            Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            _isHover = !_isHover;
            Invalidate();
        }
    }
}
