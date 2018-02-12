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
    /// Menporul RadioButton
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRadioButton : RadioButton
    {
        /// <summary>
        /// 
        /// </summary>
        private Image _imgHoverCircle =global::Menporul.Windows.Controls.MenResource.HoverCircle;
        /// <summary>
        /// 
        /// </summary>
        private Image _imgNormalCircle = global::Menporul.Windows.Controls.MenResource.NormalCircle;
        /// <summary>
        /// 
        /// </summary>
        private Image _imgDotCircle = global::Menporul.Windows.Controls.MenResource.CircleFill;
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshBackGround = new SolidBrush(MenConstants.Light);
        /// <summary>
        /// 
        /// </summary>
        private SolidBrush _bshText = new SolidBrush(MenConstants.Dark);
        /// <summary>
        /// 
        /// </summary>
        private bool _isHOver;
        /// <summary>
        /// 
        /// </summary>
        public MenRadioButton()
        {            
            this.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FlatAppearance.BorderSize = 0;
            this.AutoSize = false;
            this.Height = 23;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            //Remove Default Radiobutton Rendering            
            e.Graphics.Clear(BackColor);

            //rectangle for Radiobutton area background color           
            Rectangle backRect = new Rectangle(0,0, 18,18);  
                      
            //rectangle for  Radiobutton  Circle            
            Rectangle circleRect;

            //rectangle for  Radiobutton check mark Circle  
            Rectangle dotRect = new Rectangle(6, 6, 8, 8);

            //Draw Radiobutton area background color
            e.Graphics.FillEllipse(_bshBackGround, backRect);

           //Draw radiobutton string
            e.Graphics.DrawString(Text, Font, _bshText, 24, 4);

            // Draw Radiobutton Circle
            if (_isHOver)
            {
                circleRect = new Rectangle(1, 1, 18, 18);
                e.Graphics.DrawImage(_imgHoverCircle, circleRect);
            }
            else
            {
                circleRect = new Rectangle(2, 2, 16 , 16);
                e.Graphics.DrawImage(_imgNormalCircle, circleRect);
            }

            //Draw Check mark Circle
            if (Checked)
            {
                e.Graphics.DrawImage(_imgDotCircle, dotRect);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHOver = false;
            Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.Parent.GetType() == typeof(Panel) || this.Parent.GetType() == typeof(GroupBox))
            {
                this.Parent.Controls.OfType<MenRadioButton>().Select(o => o.Checked = false);
                this.Checked = true;
            }
            else
            {
                this.Checked = !this.Checked;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _isHOver = true;
            Invalidate();
        }
    }

}
