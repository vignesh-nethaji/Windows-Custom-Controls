using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Ribbon Button
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRibbonTab : TabControl
    {
        #region	Construction
        public MenRibbonTab()
        {
            this.Visible = true;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
            this._BackBuffer = new Bitmap(this.Width, this.Height);
            this._BackBufferGraphics = Graphics.FromImage(this._BackBuffer);
            this._TabBuffer = new Bitmap(this.Width, this.Height);
            this._TabBufferGraphics = Graphics.FromImage(this._TabBuffer);
            this._StyleProvider = new DefaultTabStyleProvider(this);
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }


        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cp = base.CreateParams;
                if (this.RightToLeftLayout)
                    cp.ExStyle = cp.ExStyle | NativeMethods.WS_EX_LAYOUTRTL | NativeMethods.WS_EX_NOINHERITLAYOUT;
                return cp;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this._BackImage != null)
                {
                    this._BackImage.Dispose();
                }
                if (this._BackBufferGraphics != null)
                {
                    this._BackBufferGraphics.Dispose();
                }
                if (this._BackBuffer != null)
                {
                    this._BackBuffer.Dispose();
                }
                if (this._TabBufferGraphics != null)
                {
                    this._TabBufferGraphics.Dispose();
                }
                if (this._TabBuffer != null)
                {
                    this._TabBuffer.Dispose();
                }

                if (this._StyleProvider != null)
                {
                    this._StyleProvider.Dispose();
                }
            }
        }

        #endregion

        #region Private variables


        private bool _isRibbon;
        private bool _isPainting;
        private Bitmap _BackImage;
        private Bitmap _BackBuffer;
        private Graphics _BackBufferGraphics;
        private Bitmap _TabBuffer;
        private Graphics _TabBufferGraphics;
        private TabStyleProvider _StyleProvider;
        private List<TabPage> _TabPages;

        #endregion

        #region Public properties


        //	Hide the Padding attribute so it can not be changed
        //	We are handling this on the Style Provider
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Point Padding
        {
            get { return this._StyleProvider.Padding; }
            set
            {
                this._StyleProvider.Padding = value;
            }
        }

        public override bool RightToLeftLayout
        {
            get { return base.RightToLeftLayout; }
            set
            {
                base.RightToLeftLayout = value;
                this.UpdateStyles();
            }
        }


        //	Hide the HotTrack attribute so it can not be changed
        //	We are handling this on the Style Provider
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool HotTrack
        {
            get { return this._StyleProvider.HotTrack; }
            set
            {
                this._StyleProvider.HotTrack = value;
            }
        }

        //	Hide the Appearance attribute so it can not be changed
        //	We don't want it as we are doing all the painting.
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public new TabAppearance Appearance
        {
            get
            {
                return base.Appearance;
            }
            set
            {
                base.Appearance = TabAppearance.Normal;
            }
        }

        [Browsable(false)]
        public int ActiveIndex
        {
            get
            {
                NativeMethods.TCHITTESTINFO hitTestInfo = new NativeMethods.TCHITTESTINFO(this.PointToClient(Control.MousePosition));
                int index = NativeMethods.SendMessage(this.Handle, NativeMethods.TCM_HITTEST, IntPtr.Zero, NativeMethods.ToIntPtr(hitTestInfo)).ToInt32();
                if (index == -1)
                {
                    return -1;
                }
                else
                {
                    if (this.TabPages[index].Enabled)
                    {
                        return index;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        [Browsable(false)]
        public TabPage ActiveTab
        {
            get
            {
                int activeIndex = this.ActiveIndex;
                if (activeIndex > -1)
                {
                    return this.TabPages[activeIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        //	Hide the HotTrack attribute so it can not be changed
        //	We are handling this on the Style Provider
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
                if (this.Parent is Panel)
                {
                    this.Parent.Height = value;
                }
            }
        }
        private bool _IsMiniMized;
        public bool IsMinimized
        {
            get
            {
                return _IsMiniMized;

            }
            set
            {
                _IsMiniMized = value;
                if (_isRibbon)
                {
                    if (_IsMiniMized)
                    {
                        this.Height = this.MinimumSize.Height;
                    }
                    else
                    {
                        this.Height = this.MaximumSize.Height;
                    }

                }
            }
        }
        #endregion

        #region	Extension methods

        public void HideTab(TabPage page)
        {
            if (page != null && this.TabPages.Contains(page))
            {
                this.BackupTabPages();
                this.TabPages.Remove(page);
            }
        }

        public void HideTab(int index)
        {
            if (this.IsValidTabIndex(index))
            {
                this.HideTab(this._TabPages[index]);
            }
        }

        public void HideTab(string key)
        {
            if (this.TabPages.ContainsKey(key))
            {
                this.HideTab(this.TabPages[key]);
            }
        }

        public void ShowTab(TabPage page)
        {
            if (page != null)
            {
                if (this._TabPages != null)
                {
                    if (!this.TabPages.Contains(page)
                        && this._TabPages.Contains(page))
                    {
                        int pageIndex = this._TabPages.IndexOf(page);
                        if (pageIndex > 0)
                        {
                            int start = pageIndex - 1;
                            for (int index = start; index >= 0; index--)
                            {
                                if (this.TabPages.Contains(this._TabPages[index]))
                                {
                                    pageIndex = this.TabPages.IndexOf(this._TabPages[index]) + 1;
                                    break;
                                }
                            }
                        }
                        if ((pageIndex >= 0) && (pageIndex < this.TabPages.Count))
                        {
                            this.TabPages.Insert(pageIndex, page);
                        }
                        else
                        {
                            this.TabPages.Add(page);
                        }
                    }
                }
                else
                {
                    if (!this.TabPages.Contains(page))
                    {
                        this.TabPages.Add(page);
                    }
                }
            }
        }

        public void ShowTab(int index)
        {
            if (this.IsValidTabIndex(index))
            {
                this.ShowTab(this._TabPages[index]);
            }
        }

        public void ShowTab(string key)
        {
            if (this._TabPages != null)
            {
                TabPage tab = this._TabPages.Find(delegate(TabPage page) { return page.Name.Equals(key, StringComparison.OrdinalIgnoreCase); });
                this.ShowTab(tab);
            }
        }

        private bool IsValidTabIndex(int index)
        {
            this.BackupTabPages();
            return ((index >= 0) && (index < this._TabPages.Count));
        }

        private void BackupTabPages()
        {
            if (this._TabPages == null)
            {
                this._TabPages = new List<TabPage>();
                foreach (TabPage page in this.TabPages)
                {
                    this._TabPages.Add(page);
                }
            }
        }

        private MenRibbonTabPage GetPageByPoint(Point point)
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                MenRibbonTabPage page = this.TabPages[i] as MenRibbonTabPage;
                if (this.GetTabRect(i).Contains(point))
                    return page;
            }
            return null;
        }
        #endregion

        #region Events

        [Category("Action")]
        public event EventHandler<TabControlEventArgs> TabImageClick;

        #endregion

        #region	Base class event processing

        protected override void OnFontChanged(EventArgs e)
        {
            IntPtr hFont = this.Font.ToHfont();
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETFONT, hFont, (IntPtr)(-1));
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
            if (this.Visible)
            {
                this.Invalidate();
            }
        }
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (_isRibbon)
            {
                var curTabPage = this.SelectedTab as MenRibbonTabPage;
                curTabPage.ControlAdding();
                if (IsMinimized && GetPageByPoint(this.PointToClient(Cursor.Position)) != null)
                {
                    IsMinimized = false;
                }
            }
            this.Refresh();
            if (!_isPainting)
            {
                Graphics g = this.CreateGraphics();
                this.CustomPaint(g);
                g.Dispose();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            if (this.Width > 0 && this.Height > 0)
            {
                if (this._BackImage != null)
                {
                    this._BackImage.Dispose();
                    this._BackImage = null;
                }
                if (this._BackBufferGraphics != null)
                {
                    this._BackBufferGraphics.Dispose();
                }
                if (this._BackBuffer != null)
                {
                    this._BackBuffer.Dispose();
                }

                this._BackBuffer = new Bitmap(this.Width, this.Height);
                this._BackBufferGraphics = Graphics.FromImage(this._BackBuffer);


                if (this._TabBufferGraphics != null)
                {
                    this._TabBufferGraphics.Dispose();
                }
                if (this._TabBuffer != null)
                {
                    this._TabBuffer.Dispose();
                }

                this._TabBuffer = new Bitmap(this.Width, this.Height);
                this._TabBufferGraphics = Graphics.FromImage(this._TabBuffer);

                if (this._BackImage != null)
                {
                    this._BackImage.Dispose();
                    this._BackImage = null;
                }

            }
            base.OnResize(e);
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            base.OnSelecting(e);

            if (e.Action == TabControlAction.Selecting && e.TabPage != null && !e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (this.Visible)
            {
                this.Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int index = this.ActiveIndex;

            if (e.Location.X <= this.Width && e.Location.X >= (this.Width - 50) && e.Location.Y >= 1 && e.Location.Y <= 20)
            {
            }
            if (!this.DesignMode && index > -1 && this._StyleProvider.ShowTabCloser && this.GetTabCloserRect(index).Contains(this.MousePosition))
            {
                TabPage tab = this.ActiveTab;
                TabControlCancelEventArgs args = new TabControlCancelEventArgs(tab, index, false, TabControlAction.Deselecting);

                if (!args.Cancel)
                {
                    this.TabPages.Remove(tab);
                    tab.Dispose();
                }
            }
            else
            {
                base.OnMouseClick(e);
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            //base.OnMouseDoubleClick(e);
            IsMinimized = !IsMinimized;
        }
        protected virtual void OnTabImageClick(TabControlEventArgs e)
        {
            if (this.TabImageClick != null)
            {
                this.TabImageClick(this, e);
            }
        }

        #endregion

        #region	Basic drawing methods

        protected override void OnPaint(PaintEventArgs e)
        {
            _isPainting = true;
            if (e.ClipRectangle.Equals(this.ClientRectangle))
            {
                this.CustomPaint(e.Graphics);
            }
            else
            {
                this.Invalidate();
            }
            _isPainting = false;
        }

        private void CustomPaint(Graphics screenGraphics)
        {
            if (this.Width > 0 && this.Height > 0)
            {
                if (this._BackImage == null)
                {
                    this._BackImage = new Bitmap(this.Width, this.Height);
                    Graphics backGraphics = Graphics.FromImage(this._BackImage);
                    backGraphics.Clear(Color.Transparent);
                    this.PaintTransparentBackground(backGraphics, this.ClientRectangle);
                }

                this._BackBufferGraphics.Clear(Color.Transparent);
                this._BackBufferGraphics.DrawImageUnscaled(this._BackImage, 0, 0);

                this._TabBufferGraphics.Clear(Color.Transparent);

                if (this.TabCount > 0)
                {
                    if (this.Alignment <= TabAlignment.Bottom && !this.Multiline)
                    {
                        this._TabBufferGraphics.Clip = new Region(new RectangleF(this.ClientRectangle.X + 3, this.ClientRectangle.Y, this.ClientRectangle.Width - 6, this.ClientRectangle.Height));
                    }
                    if (this.Multiline)
                    {
                        for (int row = 0; row < this.RowCount; row++)
                        {
                            for (int index = this.TabCount - 1; index >= 0; index--)
                            {
                                if (index != this.SelectedIndex && (this.RowCount == 1 || this.GetTabRow(index) == row))
                                {
                                    this.DrawTabPage(index, this._TabBufferGraphics);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int index = this.TabCount - 1; index >= 0; index--)
                        {
                            if (index != this.SelectedIndex)
                            {
                                this.DrawTabPage(index, this._TabBufferGraphics);
                            }
                        }
                    }

                    if (this.SelectedIndex > -1)
                    {
                        this.DrawTabPage(this.SelectedIndex, this._TabBufferGraphics);
                    }
                    if (_isRibbon)
                    {
                        //this._TabBufferGraphics.DrawString("Multi Line", this.Font, Brushes.White, new PointF(this.Width - 50, 2));
                    }
                }
                this._TabBufferGraphics.Flush();
                ColorMatrix alphaMatrix = new ColorMatrix();
                alphaMatrix.Matrix00 = alphaMatrix.Matrix11 = alphaMatrix.Matrix22 = alphaMatrix.Matrix44 = 1;
                alphaMatrix.Matrix33 = this._StyleProvider.Opacity;
                using (ImageAttributes alphaAttributes = new ImageAttributes())
                {

                    alphaAttributes.SetColorMatrix(alphaMatrix);
                    this._BackBufferGraphics.DrawImage(this._TabBuffer,
                                                       new Rectangle(0, 0, this._TabBuffer.Width, this._TabBuffer.Height),
                                                       0, 0, this._TabBuffer.Width, this._TabBuffer.Height, GraphicsUnit.Pixel,
                                                       alphaAttributes);
                }

                this._BackBufferGraphics.Flush();
                if (this.RightToLeftLayout)
                {
                    screenGraphics.DrawImageUnscaled(this._BackBuffer, -1, 0);
                }
                else
                {
                    screenGraphics.DrawImageUnscaled(this._BackBuffer, 0, 0);
                }
            }
        }

        protected void PaintTransparentBackground(Graphics graphics, Rectangle clipRect)
        {

            if ((this.Parent != null))
            {
                clipRect.Offset(this.Location);
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                graphics.SmoothingMode = SmoothingMode.HighSpeed;

                PaintEventArgs e = new PaintEventArgs(graphics, clipRect);
                try
                {
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                }
                finally
                {
                    graphics.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                }
            }
        }

        private void DrawTabPage(int index, Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            using (GraphicsPath tabPageBorderPath = this.GetTabPageBorder(index))
            {
                using (Brush fillBrush = this._StyleProvider.GetPageBackgroundBrush(index))
                {
                    graphics.FillPath(fillBrush, tabPageBorderPath);
                }
                this._StyleProvider.PaintTab(index, graphics);
                this.DrawTabText(index, graphics);
                this.DrawTabBorder(tabPageBorderPath, index, graphics);
            }
        }

        private void DrawTabBorder(GraphicsPath path, int index, Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            Color borderColor;
            if (index == this.SelectedIndex)
            {
                borderColor = this._StyleProvider.BorderColorSelected;
            }
            else if (this._StyleProvider.HotTrack && index == this.ActiveIndex)
            {
                borderColor = this._StyleProvider.BorderColorHot;
            }
            else
            {
                borderColor = this._StyleProvider.BorderColor;
            }

            using (Pen borderPen = new Pen(borderColor))
            {
                graphics.DrawPath(borderPen, path);
            }
        }

        private void DrawTabText(int index, Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Rectangle tabBounds = this.GetTabTextRect(index);

            if (this.SelectedIndex == index)
            {
                using (Brush textBrush = new SolidBrush(this._StyleProvider.TextColorSelected))
                {
                    graphics.DrawString(this.TabPages[index].Text, this.Font, textBrush, tabBounds, this.GetStringFormat());
                }
            }
            else
            {
                if (this.TabPages[index].Enabled)
                {
                    using (Brush textBrush = new SolidBrush(this._StyleProvider.TextColor))
                    {
                        graphics.DrawString(this.TabPages[index].Text, this.Font, textBrush, tabBounds, this.GetStringFormat());
                    }
                }
                else
                {
                    using (Brush textBrush = new SolidBrush(this._StyleProvider.TextColorDisabled))
                    {
                        graphics.DrawString(this.TabPages[index].Text, this.Font, textBrush, tabBounds, this.GetStringFormat());
                    }
                }
            }
        }

        #endregion

        #region String formatting

        private StringFormat GetStringFormat()
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            if (this.FindForm() != null && this.FindForm().KeyPreview)
            {
                format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            }
            else
            {
                format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
            }
            if (this.RightToLeft == RightToLeft.Yes)
            {
                format.FormatFlags = format.FormatFlags | StringFormatFlags.DirectionRightToLeft;
            }
            return format;
        }

        #endregion

        #region Tab borders and bounds properties

        private GraphicsPath GetTabPageBorder(int index)
        {

            GraphicsPath path = new GraphicsPath();
            Rectangle pageBounds = this.GetPageBounds(index);
            Rectangle tabBounds = this._StyleProvider.GetTabRect(index);
            this._StyleProvider.AddTabBorder(path, tabBounds);
            this.AddPageBorder(path, pageBounds, tabBounds);

            path.CloseFigure();
            return path;
        }

        public Rectangle GetPageBounds(int index)
        {
            Rectangle pageBounds = this.TabPages[index].Bounds;
            pageBounds.Width += 1;
            pageBounds.Height += 1;
            pageBounds.X -= 1;
            pageBounds.Y -= 1;

            if (pageBounds.Bottom > this.Height - 4)
            {
                pageBounds.Height -= (pageBounds.Bottom - this.Height + 4);
            }
            return pageBounds;
        }

        private Rectangle GetTabTextRect(int index)
        {
            Rectangle textRect = new Rectangle();
            using (GraphicsPath path = this._StyleProvider.GetTabBorder(index))
            {
                RectangleF tabBounds = path.GetBounds();

                textRect = new Rectangle((int)tabBounds.X, (int)tabBounds.Y, (int)tabBounds.Width, (int)tabBounds.Height);
                textRect.Y += 4;
                textRect.Height -= 6;

                while (!path.IsVisible(textRect.Right, textRect.Y) && textRect.Width > 0)
                {
                    textRect.Width -= 1;
                }
                while (!path.IsVisible(textRect.X, textRect.Y) && textRect.Width > 0)
                {
                    textRect.X += 1;
                    textRect.Width -= 1;
                }
            }
            return textRect;
        }

        public int GetTabRow(int index)
        {
            Rectangle rect = this.GetTabRect(index);
            int row = (rect.Y - 2) / rect.Height;
            return row;
        }

        public bool IsFirstTabInRow(int index)
        {
            if (index < 0)
            {
                return false;
            }
            bool firstTabinRow = (index == 0);
            if (!firstTabinRow)
            {
                if (this.Alignment <= TabAlignment.Bottom)
                {
                    if (this.GetTabRect(index).X == 2)
                    {
                        firstTabinRow = true;
                    }
                }
                else
                {
                    if (this.GetTabRect(index).Y == 2)
                    {
                        firstTabinRow = true;
                    }
                }
            }
            return firstTabinRow;
        }

        private void AddPageBorder(GraphicsPath path, Rectangle pageBounds, Rectangle tabBounds)
        {
            path.AddLine(tabBounds.Right, pageBounds.Y, pageBounds.Right, pageBounds.Y);
            path.AddLine(pageBounds.Right, pageBounds.Y, pageBounds.Right, pageBounds.Bottom);
            path.AddLine(pageBounds.Right, pageBounds.Bottom, pageBounds.X, pageBounds.Bottom);
            path.AddLine(pageBounds.X, pageBounds.Bottom, pageBounds.X, pageBounds.Y);
            path.AddLine(pageBounds.X, pageBounds.Y, tabBounds.X, pageBounds.Y);
        }

        private Rectangle GetTabImageRect(GraphicsPath tabBorderPath)
        {
            Rectangle imageRect = new Rectangle();
            RectangleF rect = tabBorderPath.GetBounds();
            rect.Y += 4;
            rect.Height -= 6;
            if (this.Alignment <= TabAlignment.Bottom)
            {
                if ((this._StyleProvider.ImageAlign & NativeMethods.AnyLeftAlign) != ((ContentAlignment)0))
                {
                    imageRect = new Rectangle((int)rect.X, (int)rect.Y + (int)Math.Floor((double)((int)rect.Height - 16) / 2), 16, 16);
                    while (!tabBorderPath.IsVisible(imageRect.X, imageRect.Y))
                    {
                        imageRect.X += 1;
                    }
                    imageRect.X += 4;

                }
                else if ((this._StyleProvider.ImageAlign & NativeMethods.AnyCenterAlign) != ((ContentAlignment)0))
                {
                    imageRect = new Rectangle((int)rect.X + (int)Math.Floor((double)(((int)rect.Right - (int)rect.X - (int)rect.Height + 2) / 2)), (int)rect.Y + (int)Math.Floor((double)((int)rect.Height - 16) / 2), 16, 16);
                }
                else
                {
                    imageRect = new Rectangle((int)rect.Right, (int)rect.Y + (int)Math.Floor((double)((int)rect.Height - 16) / 2), 16, 16);
                    while (!tabBorderPath.IsVisible(imageRect.Right, imageRect.Y))
                    {
                        imageRect.X -= 1;
                    }
                    imageRect.X -= 4;

                    if (this._StyleProvider.ShowTabCloser && !this.RightToLeftLayout)
                    {
                        imageRect.X -= 10;
                    }
                }
            }
            else
            {
                if ((this._StyleProvider.ImageAlign & NativeMethods.AnyLeftAlign) != ((ContentAlignment)0))
                {
                    imageRect = new Rectangle((int)rect.X + (int)Math.Floor((double)((int)rect.Width - 16) / 2), (int)rect.Y, 16, 16);
                    while (!tabBorderPath.IsVisible(imageRect.X, imageRect.Y))
                    {
                        imageRect.Y += 1;
                    }
                    imageRect.Y += 4;
                }
                else if ((this._StyleProvider.ImageAlign & NativeMethods.AnyCenterAlign) != ((ContentAlignment)0))
                {
                    imageRect = new Rectangle((int)rect.X + (int)Math.Floor((double)((int)rect.Width - 16) / 2), (int)rect.Y + (int)Math.Floor((double)(((int)rect.Bottom - (int)rect.Y - (int)rect.Width + 2) / 2)), 16, 16);
                }
                else
                {
                    imageRect = new Rectangle((int)rect.X + (int)Math.Floor((double)((int)rect.Width - 16) / 2), (int)rect.Bottom, 16, 16);
                    while (!tabBorderPath.IsVisible(imageRect.X, imageRect.Bottom))
                    {
                        imageRect.Y -= 1;
                    }
                    imageRect.Y -= 4;

                    if (this._StyleProvider.ShowTabCloser && !this.RightToLeftLayout)
                    {
                        imageRect.Y -= 10;
                    }
                }
            }
            return imageRect;
        }

        public Rectangle GetTabCloserRect(int index)
        {
            Rectangle closerRect = new Rectangle();
            using (GraphicsPath path = this._StyleProvider.GetTabBorder(index))
            {
                RectangleF rect = path.GetBounds();
                rect.Y += 4;
                rect.Height -= 6;
                if (this.RightToLeftLayout)
                {
                    closerRect = new Rectangle((int)rect.Left, (int)rect.Y + (int)Math.Floor((double)((int)rect.Height - 6) / 2), 6, 6);
                    while (!path.IsVisible(closerRect.Left, closerRect.Y) && closerRect.Right < this.Width)
                    {
                        closerRect.X += 1;
                    }
                    closerRect.X += 4;
                }
                else
                {
                    closerRect = new Rectangle((int)rect.Right, (int)rect.Y + (int)Math.Floor((double)((int)rect.Height - 6) / 2), 6, 6);
                    while (!path.IsVisible(closerRect.Right, closerRect.Y) && closerRect.Right > -6)
                    {
                        closerRect.X -= 1;
                    }
                    closerRect.X -= 4;
                }

            }
            return closerRect;
        }

        public new Point MousePosition
        {
            get
            {
                Point loc = this.PointToClient(Control.MousePosition);
                if (this.RightToLeftLayout)
                {
                    loc.X = (this.Width - loc.X);
                }
                return loc;
            }
        }

        #endregion
        #region AlignControls
        public void AlignControls()
        {
            _isRibbon = true;

            foreach (var item in this.Controls)
            {
                try
                {
                    var tab = (MenRibbonTabPage)item;
                    if (tab != null)
                    {
                        tab.AlignControls();

                    }
                }
                catch (Exception ex)
                {

                    throw;
                }


            }
            var curTabPage = this.SelectedTab as MenRibbonTabPage;
            curTabPage.ControlAdding();
        }
        #endregion
    }

    public enum TabStyle
    {
        None = 0,
        Default = 1
    }

    internal sealed class NativeMethods
    {
        private NativeMethods() { }

        #region Windows Constants

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_GETTABRECT = 0x130a;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_TRANSPARENT = 0x20;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_SETFONT = 0x30;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_FONTCHANGE = 0x1d;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_HSCROLL = 0x114;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int TCM_HITTEST = 0x130D;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_PAINT = 0xf;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_LAYOUTRTL = 0x400000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;


        #endregion

        #region Content Alignment

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyRightAlign = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyLeftAlign = ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyTopAlign = ContentAlignment.TopRight | ContentAlignment.TopCenter | ContentAlignment.TopLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyBottomAlign = ContentAlignment.BottomRight | ContentAlignment.BottomCenter | ContentAlignment.BottomLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyMiddleAlign = ContentAlignment.MiddleRight | ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyCenterAlign = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;

        #endregion

        #region User32.dll

        public static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            Control control = Control.FromHandle(hWnd);
            if (control == null)
            {
                return IntPtr.Zero;
            }

            Message message = new Message();
            message.HWnd = hWnd;
            message.LParam = lParam;
            message.WParam = wParam;
            message.Msg = msg;

            MethodInfo wproc = control.GetType().GetMethod("WndProc"
                                                           , BindingFlags.NonPublic
                                                            | BindingFlags.InvokeMethod
                                                            | BindingFlags.FlattenHierarchy
                                                            | BindingFlags.IgnoreCase
                                                            | BindingFlags.Instance);

            object[] args = new object[] { message };
            wproc.Invoke(control, args);

            return ((Message)args[0]).Result;
        }

        #endregion

        #region Misc Functions

        public static int LoWord(IntPtr dWord)
        {
            return dWord.ToInt32() & 0xffff;
        }

        public static int HiWord(IntPtr dWord)
        {
            if ((dWord.ToInt32() & 0x80000000) == 0x80000000)
                return (dWord.ToInt32() >> 16);
            else
                return (dWord.ToInt32() >> 16) & 0xffff;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static IntPtr ToIntPtr(object structure)
        {
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, lparam, false);
            return lparam;
        }


        #endregion

        #region Windows Structures and Enums

        [Flags()]
        public enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }



        [StructLayout(LayoutKind.Sequential)]
        public struct TCHITTESTINFO
        {

            public TCHITTESTINFO(Point location)
            {
                pt = location;
                flags = TCHITTESTFLAGS.TCHT_ONITEM;
            }

            public Point pt;
            public TCHITTESTFLAGS flags;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public int fErase;
            public RECT rcPaint;
            public int fRestore;
            public int fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public static RECT FromIntPtr(IntPtr ptr)
            {
                RECT rect = (RECT)Marshal.PtrToStructure(ptr, typeof(RECT));
                return rect;
            }

            public Size Size
            {
                get
                {
                    return new Size(this.right - this.left, this.bottom - this.top);
                }
            }
        }


        #endregion

    }
}
