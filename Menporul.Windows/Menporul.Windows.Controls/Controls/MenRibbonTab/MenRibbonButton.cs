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
    /// Menporul Ribbon Button
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRibbonButton : Panel
    {
        /// <summary>
        /// 
        /// </summary>
        private MenToolTip tp;
        /// <summary>
        /// 
        /// </summary>
        private MenRibbonPictureBox _primaryImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private MenRibbonPictureBox _childImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private Label Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToolTipText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override string Text
        {
            get { return this.Title.Text; }
            set
            {
                this.Title.Text = value;              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private MenRibbonButtonType _buttonStyle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MenRibbonButtonType ButtonStyle
        {
            get
            {
                return _buttonStyle;
            }
            set
            {
                _buttonStyle = value;
                ButtonnStyleOnChange();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _isChild { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsChild
        {
            get
            {
                return _isChild;
            }
            set
            {
                _isChild = value;
                IsChildOnChnage();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ContextMenu ContextMenuCollections { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                this._primaryImage.Enabled = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (true)
                {

                }
                base.Visible = value;
                this.Visible2 = value;                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _visible;
        /// <summary>
        /// 
        /// </summary>
        internal bool Visible2
        {
            get
            {
                return this._visible;
            }
            private set
            {
                this._visible = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image PrimaryImage
        {
            get
            {
                return _primaryImage.Image;
            }
            set
            {
                this._primaryImage.Image = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image ChildImage
        {
            get
            {
                return global::Menporul.Windows.Controls.MenResource.Expand;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public MenRibbonButton()
        {

            this.ContextMenuCollections = new ContextMenu();
            tp = new MenToolTip();
            
            this.Title = new Label();
            this.Title.Name = "ButtonTitle";
            this.Title.TextAlign = ContentAlignment.MiddleCenter;
            this.Title.AutoSize = false;
            this.Title.Font = MenRibbonConstants.TextFont;
            this.Title.MouseEnter += new EventHandler(this.MouseEnter);
            this.Title.MouseLeave += new EventHandler(this.MouseLeave);
            this.Title.Click += new EventHandler(this.MouseCLick);
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);

            this._primaryImage = new MenRibbonPictureBox();
            this._primaryImage.Name = "PrimaryImage";
            this._primaryImage.SizeMode = PictureBoxSizeMode.CenterImage;
            this._primaryImage.TabStop = false;
            this._primaryImage.MouseEnter += new EventHandler(this.MouseEnter);
            this._primaryImage.MouseLeave += new EventHandler(this.MouseLeave);
            this._primaryImage.Click += new EventHandler(this.MouseCLick);
            this._primaryImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);

            this._childImage = new MenRibbonPictureBox(MenRibbonConstants.ChildImageSize);



            base.Enabled = true;
            this._visible = true;
            this.Location = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.Y);
            this.Name = "RibbonButton";
            this.Controls.Add(this._primaryImage);
            this.Controls.Add(this.Title);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonStyle"></param>
        /// <param name="isChild"></param>
        public MenRibbonButton(MenRibbonButtonType buttonStyle, bool isChild)
            : this()
        {
            this.ButtonStyle = buttonStyle;
            this.IsChild = isChild;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textWidth"></param>
        /// <returns></returns>
        private Size GetFinalSize(string text, int textWidth)
        {
            Size size = TextRenderer.MeasureText(this.Title.Text, this.Title.Font, new Size(this.Width - MenRibbonConstants.SmallMarginBetweenControls * 2, 22), TextFormatFlags.WordBreak);
            size.Width = textWidth > size.Width ? textWidth : size.Width;

            return size;
        }
        /// <summary>
        /// 
        /// </summary>
        private void IsChildOnChnage()
        {
            if (this.IsChild && !this.Controls.ContainsKey("ChildImage"))
            {
                this._childImage.Image = global::Menporul.Windows.Controls.MenResource.FMMoreMenu;
                this._childImage.Name = "ChildImage";
                this._childImage.TabStop = false;
                this._childImage.MouseEnter += new EventHandler(this.MouseEnter);
                this._childImage.MouseLeave += new EventHandler(this.MouseLeave);
                this._childImage.Click += new EventHandler(this.MouseCLick);
                this._childImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
                this.Controls.Add(this._childImage);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public void ButtonnStyleOnChange()
        {
            if (this.ButtonStyle == MenRibbonButtonType.Large)
            {
                this.Size = new System.Drawing.Size(MenRibbonConstants.LargeBtnWidth, MenRibbonConstants.LargeBtnHeight);
                this._primaryImage.Size = new Size(MenRibbonConstants.LargePrimaryImageSize, MenRibbonConstants.LargePrimaryImageSize);
                this._primaryImage.Location = new System.Drawing.Point(MenRibbonConstants.LargePrimaryImageX, MenRibbonConstants.Y);
                this.Title.Location = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.LargePrimaryImageSize + MenRibbonConstants.BigMarginBetweenControl);
            }
            else if (this.ButtonStyle == MenRibbonButtonType.Compact)
            {
                this._primaryImage.Size = new Size(MenRibbonConstants.CompactPrimaryImageSize, MenRibbonConstants.CompactPrimaryImageSize);
                this._primaryImage.Location = new System.Drawing.Point(MenRibbonConstants.X + 1, MenRibbonConstants.Y + 1);
                this.Title.Location = new System.Drawing.Point(MenRibbonConstants.CompactPrimaryImageSize + MenRibbonConstants.BigMarginBetweenControl, 4);
            }
            else if (this.ButtonStyle == MenRibbonButtonType.LargeText)
            {
                this.Size = new System.Drawing.Size(MenRibbonConstants.LargeTextBtnWidth, MenRibbonConstants.LargeBtnHeight);
                this._primaryImage.Size = new Size(MenRibbonConstants.LargePrimaryImageSize, MenRibbonConstants.LargePrimaryImageSize);
                this._primaryImage.Location = new System.Drawing.Point(MenRibbonConstants.LargeTextPrimaryImageX, MenRibbonConstants.Y);
                this.Title.Location = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.LargePrimaryImageSize + MenRibbonConstants.BigMarginBetweenControl);
            }
            else if (this.ButtonStyle == MenRibbonButtonType.LargeImageText)
            {
                this.Size = new System.Drawing.Size(MenRibbonConstants.LargeImageTextBtnWidth, MenRibbonConstants.LargeBtnHeight);
                this._primaryImage.Size = new Size(MenRibbonConstants.LargeImageTextPrimaryImageSize, MenRibbonConstants.LargePrimaryImageSize);
                this._primaryImage.Location = new System.Drawing.Point(MenRibbonConstants.LargeImageTextPrimaryImageX, MenRibbonConstants.Y);
                this.Title.Location = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.LargePrimaryImageSize + MenRibbonConstants.BigMarginBetweenControl);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Size textSize = e.Graphics.MeasureString(this.Title.Text, this.Title.Font).ToSize();
            int textWidth = textSize.Width;
            int textHeight = textSize.Height;
            if (this.ButtonStyle == MenRibbonButtonType.Compact)
            {
                int btnWidth = MenRibbonConstants.BigMarginBetweenControl + textWidth + this.PrimaryImage.Width;
                Size finalSize = GetFinalSize(this.Title.Text, textSize.Width);
                this.Title.MaximumSize = this.Title.Size = finalSize;
                if (this.IsChild)
                {
                    this._childImage.Location = new System.Drawing.Point(finalSize.Width + MenRibbonConstants.CompactPrimaryImageSize + MenRibbonConstants.BigMarginBetweenControl * 2 + MenRibbonConstants.SmallMarginBetweenControls, this.Title.Height - 5);
                    this.Size = new System.Drawing.Size(this._childImage.Location.X + MenRibbonConstants.ChildImageSize + MenRibbonConstants.SmallMarginBetweenControls * 2, MenRibbonConstants.CompactBtnHeight);
                }
                else
                {
                    this.Size = new System.Drawing.Size(btnWidth + MenRibbonConstants.MarginBetweenCompactControls * 2 + MenRibbonConstants.CompactMoreImageWidth, MenRibbonConstants.CompactBtnHeight);
                }
            }

            else if (this.ButtonStyle == MenRibbonButtonType.Large)
            {
                textWidth = this.Width - MenRibbonConstants.SmallMarginBetweenControls * 2;
                Size finalSize = GetFinalSize(this.Title.Text, textWidth);
                if (this.IsChild)
                {
                    finalSize.Width = finalSize.Width - MenRibbonConstants.CompactMoreImageWidth;
                    Size renderedTextSize = TextRenderer.MeasureText(this.Title.Text, this.Title.Font, new Size(20, 10), TextFormatFlags.WordBreak);
                    bool multiline = this.Title.Height - this.Title.Padding.Top - this.Title.Padding.Bottom > this.Title.Font.Size * 2;
                    if (multiline)
                    {
                        if (renderedTextSize.Width > finalSize.Width)
                        {
                            this._childImage.Location = new System.Drawing.Point(finalSize.Width - MenRibbonConstants.ChildImageSize - MenRibbonConstants.SmallMarginBetweenControls, this.Title.Location.Y + this.Title.Height - 12);

                        }
                        else
                        {
                            this._childImage.Location = new System.Drawing.Point(finalSize.Width - (finalSize.Width - renderedTextSize.Width) / 2, this.Title.Location.Y + this.Title.Height - 12);
                        }
                    }
                    else
                    {
                        this._childImage.Location = new System.Drawing.Point((this.Width - this.ChildImage.Width) / 2, this._primaryImage.Height + this.Title.Height + MenRibbonConstants.BigMarginBetweenControl * 2);
                    }

                    Point tempLocation = this.Title.Location;
                    tempLocation.X += MenRibbonConstants.CompactMoreImageWidth;
                }
                this._primaryImage.Location = new Point((this.Width - this._primaryImage.Width) / 2, this._primaryImage.Location.Y);
                this.Title.MaximumSize = this.Title.Size = finalSize;
            }
            else if (this.ButtonStyle == MenRibbonButtonType.LargeText)
            {
                textWidth = this.Width - MenRibbonConstants.SmallMarginBetweenControls * 2;
                Size finalSize = GetFinalSize(this.Title.Text, textWidth);
                if (this.IsChild)
                {
                    finalSize.Width = finalSize.Width - MenRibbonConstants.CompactMoreImageWidth;
                    Size renderedTextSize = TextRenderer.MeasureText(this.Title.Text, this.Title.Font, new Size(20, 10), TextFormatFlags.WordBreak);
                    bool multiline = this.Title.Height - this.Title.Padding.Top - this.Title.Padding.Bottom > this.Title.Font.Size * 2;
                    if (multiline)
                    {
                        if (renderedTextSize.Width > finalSize.Width)
                        {
                            this._childImage.Location = new System.Drawing.Point(finalSize.Width - MenRibbonConstants.ChildImageSize - MenRibbonConstants.SmallMarginBetweenControls, this.Title.Location.Y + this.Title.Height - 12);
                        }
                        else
                        {
                            this._childImage.Location = new System.Drawing.Point(finalSize.Width, this.Title.Location.Y + this.Title.Height - 12);

                        }
                    }
                    else
                    {
                        this._childImage.Location = new System.Drawing.Point((this.Width - this.ChildImage.Width) / 2, this._primaryImage.Height + this.Title.Height + MenRibbonConstants.BigMarginBetweenControl * 2);
                    }
                    Point tempLocation = this.Title.Location;
                    tempLocation.X += MenRibbonConstants.CompactMoreImageWidth;
                }
                this._primaryImage.Location = new Point((this.Width - this._primaryImage.Width) / 2, this._primaryImage.Location.Y);
                this.Title.MaximumSize = this.Title.Size = finalSize;
            }
            else if (this.ButtonStyle == MenRibbonButtonType.LargeImageText)
            {
                if (textWidth > (this.Width - 5))
                {
                    textWidth = this.Width - MenRibbonConstants.SmallMarginBetweenControls * 2;
                }
                Size finalSize = GetFinalSize(this.Title.Text, textWidth);
                this.Title.MaximumSize = this.Title.Size = finalSize;
                this._primaryImage.Width = this._primaryImage.Image.Width;
                this._primaryImage.Location = new Point((this.Width - this._primaryImage.Width) / 2, this._primaryImage.Location.Y);
                if (this.IsChild)
                {
                    Size renderedTextSize = TextRenderer.MeasureText(this.Title.Text, this.Title.Font, new Size(textWidth, 10), TextFormatFlags.WordBreak);
                    this._childImage.Location = new System.Drawing.Point(finalSize.Width - (finalSize.Width - renderedTextSize.Width) / 2, this.Title.Location.Y + this.Title.Height - 12);
                }
            }
            this._childImage.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ChildControlRefresh()
        {
            this._primaryImage.Refresh();
            this.Title.Refresh();
            this._childImage.Refresh();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.ButtonStyle == MenRibbonButtonType.Large)
                {
                    this.ContextMenuCollections.Show(this, new Point(2, (this.Location.Y + this.Height)));
                }
                else
                {
                    this.ContextMenuCollections.Show(this, new Point(this._primaryImage.Location.X, (this.Height)));
                }
                //this.contextMenuStrip1.Show(this, new Point(e.X,e.Y);
                //this.contextMenuStrip1.Show(PointToScreen(e.Location));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = MenConstants.Thin;
            this.ChildControlRefresh();
            tp.Hide(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = MenConstants.LightGrey;
            this.ChildControlRefresh();
            if (this.Text != "")
            {
                tp.Show(this.Text, this, this.PointToClient(new Point(Cursor.Position.X + 10, Cursor.Position.Y + 30)));
            }
            else
            {
                tp.Show(this.ToolTipText, this, this.PointToClient(new Point(Cursor.Position.X + 10, Cursor.Position.Y + 30)));
            }
            
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MouseCLick(object sender, EventArgs e)
        {
            OnClick(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
    }
}
