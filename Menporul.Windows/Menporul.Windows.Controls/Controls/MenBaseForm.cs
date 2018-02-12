using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul baseForm
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public partial class MenBaseForm : Form
    {
        #region Properties and Variables
        /// <summary>
        /// 
        /// </summary>
        private MenTitlebar _MenTitleBar;
        /// <summary>
        /// 
        /// </summary>
        private MenTitlebar MenTitleBar
        {
            get
            {
                if (_MenTitleBar == null)
                {
                    _MenTitleBar = this.Controls.OfType<MenTitlebar>().FirstOrDefault();
                }
                return _MenTitleBar;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Design")]
        public bool EnableMinMax
        {
            get
            {
                return MenTitleBar.EnableMinMax;
            }
            set
            {
                MenTitleBar.EnableMinMax = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _midText;
        /// <summary>
        /// 
        /// </summary>
        public string MidText
        {
            get
            {
                return _midText;
            }
            set
            {
                _midText = value;
                this.MenTitlebar1.MidTitle = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool IsMouseDown = false;
        /// <summary>
        /// 
        /// </summary>
        private bool IsHalfMax = false;
        #endregion

        #region Form Constrcutor
        /// <summary>
        /// 
        /// </summary>
        public MenBaseForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = MenConstants.FormBackGround;
        }
        #endregion

        #region Control Added
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
           
            if (e.Control.Dock == DockStyle.Top)
            {
                int top = 0;
                foreach (Control item in this.Controls)
                {
                    if (item.Dock == DockStyle.Top && item != e.Control)
                    {
                        top = this.Controls.IndexOf(e.Control);
                        this.Controls.SetChildIndex(e.Control, 0);
                        break;

                    }
                }
            }
             int botSplitIndex = this.Controls.IndexOfKey("bottomResizeSplitter");
            if (e.Control.Dock == DockStyle.Bottom  && (this.Controls.IndexOf(e.Control) - 1) == botSplitIndex)
            {
                this.Controls.SetChildIndex(e.Control,botSplitIndex);
            }
            else if (e.Control.Dock == DockStyle.Bottom && this.Controls.IndexOf(e.Control) > botSplitIndex)
            {
                  this.Controls.SetChildIndex(e.Control,botSplitIndex);
            }
        }
        #endregion

        #region Form Events [Load, Resize , ResizeEnd, Location Change]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenBaseForm_Load(object sender, EventArgs e)
        {
            MenTitleBar.OldSize = this.Size;
            MenTitleBar.OldLocation = this.Location;
            MenTitleBar.Title = this.Text;
            MenTitleBar.MidTitle = this.MidText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenBaseForm_Resize(object sender, EventArgs e)
        {
            if (MenTitleBar != null)
            {
                if (MenTitleBar.CurrentWindowsState == MenWindowsState.Normal && !MenTitleBar.IsHalfMaxMin && !this.IsHalfMax)
                {
                    MenTitleBar.OldSize = this.Size;
                    if (this.Location != Point.Empty)
                    {
                        MenTitleBar.OldLocation = this.Location;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenBaseForm_ResizeEnd(object sender, EventArgs e)
        {
            if (MenTitleBar.CurrentWindowsState == MenWindowsState.Normal)
            {
                if (!MenTitleBar.IsLeftRight() && !IsHalfMax)
                {
                    MenTitleBar.OldSize = this.Size;
                    MenTitleBar.OldLocation = this.Location;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenBaseForm_LocationChanged(object sender, EventArgs e)
        {
            if (MenTitleBar.IsMaximizeCalled)
            {
                MenTitleBar.IsMaximizeCalled = false;
                return;
            }
            if (this.Location.Y == this.MenTitleBar.CurrentCursorWorkingArea.Y && MenTitleBar.EnableMinMax && !MenTitleBar.IsHalfMaxMin)
            {
                MenTitleBar.maximizeBtn_Click(sender, e);
            }
            else if (MenTitleBar.CurrentWindowsState == MenWindowsState.Maximized && ((this.Location.Y - MenTitleBar.OldLocation.Y != 0) || (this.Location.X - MenTitleBar.OldLocation.X != 0)))
            {
                if (this.Location.X < -1000)
                {
                    MenTitleBar.OldLocation = new Point(0, 0);
                }
                else
                {
                    MenTitleBar.OldLocation = this.Location;
                }
                MenTitleBar.maximizeBtn_Click(sender, e);
            }
        }
        #endregion

        #region Native Methods Operations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            int x = (int)(m.LParam.ToInt64() & 0xFFFF);
            int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
            Point pt = this.PointToClient(Cursor.Position);
            Size clientSize = this.ClientSize;

            if (m.Msg == MenConstants.WM_LBUTTONDOWN || m.Msg == MenConstants.WM_NCLBUTTONDOWN)
            {
                if ((pt.X >= 2 && pt.X <= clientSize.Width - 16 && pt.Y >= 2 && pt.Y <= 32) && y > 8 && this.MenTitleBar.EnableMinMax)
                {
                    IsHalfMax = (this.Location.Y == this.MenTitleBar.CurrentCursorWorkingArea.Y);
                    IsMouseDown = true;
                }
            }
            if (m.Msg == MenConstants.WM_MOUSEMOVE || m.Msg == MenConstants.WM_NCMOUSEMOVE)
            {
                if (IsMouseDown && (pt.X >= 2 && pt.X <= clientSize.Width - 16 && pt.Y >= 2 && pt.Y <= 32) && y > 8 && this.MenTitleBar.EnableMinMax)
                {
                    if (IsHalfMax && MenTitleBar.CurrentWindowsState == MenWindowsState.Normal && this.Size != MenTitleBar.OldSize)
                    {
                        this.Size = MenTitleBar.OldSize;
                    }
                    MenTitleBar.HalfMaximize(IsHalfMax);
                    IsMouseDown = false;
                }
            }
            //Client Area Double Click based on the Cursor position fires screen maximize or minimize operation
            if (m.Msg == MenConstants.WM_LBUTTONDBLCLK)
            {
                if (MenTitleBar.EnableMinMax)
                {
                    if (pt.X >= 6 && pt.X <= clientSize.Width - 16 && pt.Y >= 6 && pt.Y <= 32)
                    {
                        MenTitleBar.maximizeBtn_Click(null, null);
                    }
                    return;
                }
            }

            //Non Client  Area  - Fires the Screen Maximize or Minimize operation
            if (m.Msg == MenConstants.WM_NCLBUTTONDBLCLK)
            {
                if (MenTitleBar.EnableMinMax)
                {
                    MenTitleBar.maximizeBtn_Click(null, null);
                    return;
                }
                else
                {
                    return;
                }
            }
            //WM_NCHITTEST is used to Control Tranparency and stop the control events and allow only forms events will be fire.
            if (m.Msg == MenConstants.WM_NCHITTEST)
            {

                // set the Form Cation area or Titlebar area 
                if (pt.X >= 2 && pt.X <= clientSize.Width - 16 && pt.Y >= 2 && pt.Y <= 32)
                {
                    m.Result = (IntPtr)(MenConstants.HT_CAPTION);
                    return;
                }
                if (MenTitleBar.CurrentWindowsState == MenWindowsState.Normal)
                {
                    ///allow resize on the lower right corner
                    if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(this.IsMirrored ? MenConstants.HT_BOTTOMLEFT : MenConstants.HT_BOTTOMRIGHT);
                        return;
                    }
                    ///allow resize on the lower left corner
                    else if (pt.X <= 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(this.IsMirrored ? MenConstants.HT_BOTTOMRIGHT : MenConstants.HT_BOTTOMLEFT);
                        return;
                    }
                    ///allow resize on the upper right corner
                    else if (pt.X <= 16 && pt.Y <= 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(this.IsMirrored ? MenConstants.HT_TOPRIGHT : MenConstants.HT_TOPLEFT);
                        return;
                    }
                    ///allow resize on the upper left corner
                    else if (pt.X >= clientSize.Width - 16 && pt.Y <= 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(this.IsMirrored ? MenConstants.HT_TOPLEFT : MenConstants.HT_TOPRIGHT);
                        return;
                    }
                    ///allow resize on the top border
                    else if (pt.Y <= 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(MenConstants.HT_TOP);
                        return;
                    }
                    ///allow resize on the bottom border
                    else if (pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(MenConstants.HT_BOTTOM);
                        return;
                    }
                    ///allow resize on the left border
                    else if (pt.X <= 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(MenConstants.HT_LEFT);
                        return;
                    }
                    ///allow resize on the right border
                    else if (pt.X >= clientSize.Width - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(MenConstants.HT_RIGHT);
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
