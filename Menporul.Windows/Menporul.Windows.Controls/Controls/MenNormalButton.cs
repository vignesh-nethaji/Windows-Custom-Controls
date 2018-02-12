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
    /// MenNormalButton is Default button. It has override the FlatAButton Appearnce and Color Appearance based on Event
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenNormalButton : Button
    {
        /// <summary>
        /// Default Border Color of Button
        /// </summary>
        private Color BorderColor;
        /// <summary>
        /// Bottom Border Color When Button has no Event
        /// </summary>
        private Color BottomBorderColor;
        /// <summary>
        /// HoverBOrderColor for When hover event on button
        /// </summary>
        private Color HOverBorderColor;
        /// <summary>
        /// ActiveBorderColor for Whenever click on the button . this color will apear on border.
        /// </summary>
        private Color ActiveBorderColor;
        /// <summary>
        /// this color for background color on button;
        /// </summary>
        private Color BackGroundColor;
        /// <summary>
        /// Design the MenNormalButton APpearnce on load
        /// </summary>
        /// <summary>
        ///constrctor call initialize method for set default property values for MenNormalButton
        /// </summary>
        public MenNormalButton()
        {
            BorderColor = MenConstants.BtnNormalBorder;
            BottomBorderColor = MenConstants.BtnNormalBottomBorder;
            HOverBorderColor = MenConstants.BtnNormalHOverBorder;
            ActiveBorderColor = MenConstants.BtnNormalActiveBorder;
            BackGroundColor = MenConstants.BtnNormalBackGround;
            Initialize();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = BorderColor;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = MenConstants.BtnMouseDownBackGround;
            this.FlatAppearance.MouseOverBackColor = MenConstants.BtnMouseOverBackGround;
            this.BackColor = BackGroundColor;
            this.ForeColor = MenConstants.BtnNormalText;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;

        }
        /// <summary>
        /// Hover override for change the Flat Border Color Appearance
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            this.FlatAppearance.BorderColor = HOverBorderColor;
            this.Invalidate();
        }
        /// <summary>
        /// MouseLeave override for change the Flat Border Color Appearance
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.FlatAppearance.BorderColor = BorderColor;
            this.Invalidate();
        }
        /// <summary>
        /// MouseLeave override for change the Flat Border Color Appearance
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.FlatAppearance.BorderColor = ActiveBorderColor;
            this.Invalidate();

        }
        /// <summary>
        /// based on the events the control border appearance will change.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.FlatAppearance.BorderColor == BorderColor)
            {
                if (Enabled)
                {
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                        BorderColor, 1, ButtonBorderStyle.Outset,
                        BorderColor, 1, ButtonBorderStyle.Outset,
                        BorderColor, 1, ButtonBorderStyle.Inset,
                        BottomBorderColor, 1, ButtonBorderStyle.Solid);
                }
                else
                {
                    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                     BorderColor, 1, ButtonBorderStyle.Outset,
                     BorderColor, 1, ButtonBorderStyle.Outset,
                     BorderColor, 1, ButtonBorderStyle.Inset,
                     BottomBorderColor, 1, ButtonBorderStyle.Inset);
                }
            }
            else if (this.FlatAppearance.BorderColor == ActiveBorderColor)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                     ActiveBorderColor, 1, ButtonBorderStyle.Outset,
                                     ActiveBorderColor, 1, ButtonBorderStyle.Outset,
                                     ActiveBorderColor, 1, ButtonBorderStyle.Inset,
                                     ActiveBorderColor, 1, ButtonBorderStyle.Inset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                     HOverBorderColor, 1, ButtonBorderStyle.Outset,
                                     HOverBorderColor, 1, ButtonBorderStyle.Outset,
                                     HOverBorderColor, 1, ButtonBorderStyle.Inset,
                                     HOverBorderColor, 1, ButtonBorderStyle.Inset);
            }
        }
    }
}
