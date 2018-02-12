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
    /// Menporul CheckedListBox
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenCheckedListBox : CheckedListBox
    {
        /// <summary>
        /// 
        /// </summary>
        private Brush _bshCheckBoxBackGround = new SolidBrush(MenConstants.Light);
        /// <summary>
        /// 
        /// </summary>
        private Pen _penCheckBoxBorder = new Pen(MenConstants.ChkLstCheckBoxBorder);
        /// <summary>
        /// 
        /// </summary>
        private Image _imgCheckMark = global::Menporul.Windows.Controls.MenResource.tick;
        /// <summary>
        /// 
        /// </summary>
        private Brush _bshText = new SolidBrush(MenConstants.Dark);
        /// <summary>
        /// 
        /// </summary>
        private Brush _bshSelectedText = new SolidBrush(MenConstants.Light);
        /// <summary>
        /// 
        /// </summary>
        public MenCheckedListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            DoubleBuffered = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                if (e.Index >= 0)
                {
                    // Draw Background When Item Select
                    e.DrawBackground();
                    //Get The Default Checkbox Size
                    Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                    int dx = (e.Bounds.Height - checkSize.Width) / 2;

                    //checkbox Rectangle
                    Rectangle rect = new Rectangle(new Point(dx, e.Bounds.Top + dx), new Size(12, 12));

                    //Draw Checkbox Background
                    e.Graphics.FillRectangle(_bshCheckBoxBackGround, rect);
                    //Draw Checkbox Border
                    e.Graphics.DrawRectangle(_penCheckBoxBorder, rect);
                    if (GetItemChecked(e.Index))
                    {
                        //Draw Check mark when the item is checked
                        e.Graphics.DrawImage(_imgCheckMark, rect);

                    }
                    // Draw item text
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, (e.State & DrawItemState.Selected) == DrawItemState.Selected ? _bshSelectedText : _bshText, new Rectangle(e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height), StringFormat.GenericDefault);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
        }
    }
}
