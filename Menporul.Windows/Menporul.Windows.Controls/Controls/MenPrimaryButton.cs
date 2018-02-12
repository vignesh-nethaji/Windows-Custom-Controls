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
    /// Menporul PrimaryButton
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenPrimaryButton : Button
    {
        #region Color Declartion

        /// /// <summary>
        /// Default Border Color of Button
        /// </summary>
        private Color BorderColor = Core.MenConstants.BtnPrimaryBorder;
        /// <summary>
        /// Bottom Border Color When Button has no Event
        /// </summary>
        private Color BottomBorderColor = Core.MenConstants.BtnPrimaryBottomBorder;
        /// <summary>
        /// HoverBOrderColor for When hover event on button
        /// </summary>
        private Color HOverBorderColor = Core.MenConstants.BtnPrimaryHOverBorder;
        /// <summary>
        /// HoverBOrderColor for When hover event on button
        /// </summary>
        private Color HOverBackColor = Core.MenConstants.BtnPrimaryHOverBack;
        /// <summary>
        /// ActiveBorderColor for Whenever click on the button . this color will apear on border.
        /// </summary>
        private Color ActiveBorderColor = Core.MenConstants.BtnPrimaryActiveBorder;
        /// <summary>
        /// Active Color 
        /// </summary>
        private Color ActiveColor = Core.MenConstants.BtnPrimaryActive;
        /// <summary>
        /// this color for background color on button;
        /// </summary>
        private Color BackGroundColor = Core.MenConstants.BtnPrimaryBackGround;
        /// <summary>
        /// this color
        /// <summary>
        private Color DisabledBackColor = Core.MenConstants.BtnPrimaryDisabledBack;
        /// <summary>
        /// Disable Border Color
        /// </summary>
        private Color DisabledBorderColor = Core.MenConstants.BtnPrimaryDisabledBorder;
        /// <summary>
        /// Disable text Color
        /// </summary>
        private Color DisabledTextColor = Core.MenConstants.BtnPrimaryDisabledText;
        /// <summary>
        /// Normal Text Color 
        /// </summary>
        private Color TextColor = Core.MenConstants.BtnPrimaryTextColor;
        #endregion

        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                if (base.Enabled)
                {
                    this.BackColor = BackGroundColor;
                    this.FlatAppearance.BorderColor = BorderColor;
                    this.ForeColor = TextColor;
                }
                else
                {
                    this.BackColor = DisabledBackColor;
                    this.FlatAppearance.BorderColor = DisabledBorderColor;
                    this.ForeColor = DisabledTextColor;
                }
            }
        }
        #region Constructor
        /// <summary>
        ///constrctor call initialize method for set default property values for MenPrimaryButton
        /// </summary>
        public MenPrimaryButton()
        {
            Initialize();
        }
        #endregion
        ///
        /// Design the MenPrimaryButton Appearnce on load
        /// </summary>
        private void Initialize()
        {
            #region button property Initialization
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = BorderColor;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = ActiveColor;
            this.FlatAppearance.MouseOverBackColor = HOverBackColor;
            this.BackColor = BackGroundColor;
            this.ForeColor = TextColor;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            #endregion
        }
        #region override button Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.FlatAppearance.BorderColor = ActiveBorderColor;
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
            if (this.Enabled)
            {
                // this.FlatAppearance.BorderColor = ActiveBorderColor;
            }
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
                     BorderColor, 1, ButtonBorderStyle.Inset);
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
        #endregion
    }
}
