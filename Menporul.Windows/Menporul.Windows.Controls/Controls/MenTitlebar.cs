using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul TitleBar 
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public partial class MenTitlebar : UserControl
    {
        #region Custom TitleBar PropertiesSS
        /// <summary>
        /// 
        /// </summary>
        private MenToolTip formToolTip;
        /// <summary>
        /// 
        /// </summary>
        public MenWindowsState CurrentWindowsState;
        /// <summary>
        /// 
        /// </summary>
        public Size _oldSize;
        /// <summary>
        /// 
        /// </summary>
        public Size OldSize
        {
            get
            {
                return _oldSize;
            }
            set
            {                
                if (_oldSize != value)
                {
                    _oldSize = value;
                }
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Point _oldLocation;
        /// <summary>
        /// 
        /// </summary>
        public Point OldLocation
        {
            get
            {
                return _oldLocation;
            }
            set
            {
                if (CurrentCursorWorkingArea.Location != value)
                {
                    _oldLocation = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsMaximizeCalled;
        /// <summary>
        /// 
        /// </summary>
        private bool dockFlag;
        /// <summary>
        /// 
        /// </summary>
        private bool IsMinimize;
        /// <summary>
        /// 
        /// </summary>
        private bool _enableMinMax;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Design")]
        public bool EnableMinMax
        {
            get
            {
                return _enableMinMax;
            }
            set
            {
                _enableMinMax = value;
                this.minimizeBtn.Visible = value;
                this.maximizeBtn.Visible = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public bool IsHalfMaxMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Design")]
        public string Title
        {
            get
            {
                return this.appTitlelbl.Text;
            }
            set
            {
                this.appTitlelbl.Text = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Design")]
        public string MidTitle
        {
            get
            {
                return this.appMidTitlelbl.Text;
            }
            set
            {
                this.appMidTitlelbl.Text = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Rectangle _currentCursorWorkingArea;
        /// <summary>
        /// 
        /// </summary>
        public Rectangle CurrentCursorWorkingArea
        {
            get
            {
                var curCursor = Cursor.Position;
                foreach (Screen scr in Screen.AllScreens)
                {
                    Rectangle workingArea = scr.WorkingArea;
                    if ((workingArea.X <= curCursor.X) && (workingArea.X + workingArea.Width) >= curCursor.X)
                    {
                        _currentCursorWorkingArea = workingArea;
                        break;
                    }
                }
                return _currentCursorWorkingArea;
            }
        }
        #endregion

        #region Constructor Initialization
        /// <summary>
        /// 
        /// </summary>
        public MenTitlebar()
        {
            InitializeComponent();
            this.minimizeBtn.Visible = _enableMinMax;
            this.maximizeBtn.Visible = _enableMinMax;
            formToolTip = new MenToolTip();
            CurrentWindowsState = MenWindowsState.Normal;
            ColorInitialize();
        }
        #endregion

        #region  Color Initialization
        /// <summary>
        /// 
        /// </summary>
        private void ColorInitialize()
        {
            this.BackColor = MenConstants.Light;
            this.titleBarPanel.BackColor = MenConstants.Dark;
            this.minimizeBtn.BackColor = MenConstants.Dark;
            this.maximizeBtn.BackColor = MenConstants.Dark;
            this.closeBtn.BackColor = MenConstants.Dark;
            this.appTitlelbl.ForeColor = MenConstants.Light;
        }
        #endregion

        #region Custom Methods [HalfMaximize , IsLeftRight]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isHalfMax"></param>
        public void HalfMaximize(bool isHalfMax)
        {
            var cursorPos = Cursor.Position;
            var workingArea = CurrentCursorWorkingArea;

            if (cursorPos.X < (CurrentCursorWorkingArea.X + 5))
            {
                this.IsHalfMaxMin = true;
                this.ParentForm.Location = CurrentCursorWorkingArea.Location;
                this.ParentForm.Size = new Size(CurrentCursorWorkingArea.Width / 2, CurrentCursorWorkingArea.Height);
            }
            else if (((workingArea.X + workingArea.Width) - 5) < cursorPos.X)
            {
                this.IsHalfMaxMin = true;
                this.ParentForm.Location = new Point(workingArea.X + (workingArea.Width / 2), workingArea.Y);
                this.ParentForm.Size = new Size(workingArea.Width / 2, workingArea.Height);
            }
            else if (isHalfMax && this.CurrentWindowsState == MenWindowsState.Normal && this.ParentForm.Size != OldSize)
            {
                // this.ParentForm.Size = OldSize;
            }

            this.IsHalfMaxMin = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLeftRight()
        {
            bool result = false;
            var workingArea = CurrentCursorWorkingArea;
            var curPos = Cursor.Position;
            if ((workingArea.X + workingArea.Width) >= curPos.X)
            {
                if (curPos.X <= (workingArea.X + 5))
                {
                    result = true;
                }
                else if (((workingArea.X + workingArea.Width) - 5) < curPos.X)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region Titlebar Operations like form close ,minimize and maximize has handling custom
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            if (CurrentWindowsState == MenWindowsState.Maximized)
            {
                OldLocation = new Point(0, 0);
                IsMinimize = true;
            }
            this.ParentForm.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void maximizeBtn_Click(object sender, EventArgs e)
        {
            IsMaximizeCalled = true;
            if (this.CurrentWindowsState == MenWindowsState.Maximized && !IsMinimize)
            {

                this.CurrentWindowsState = MenWindowsState.Normal;
                this.ParentForm.Size = this.OldSize;
                if (!(OldLocation == new Point(0, 0)))
                {
                    this.ParentForm.Location = OldLocation;
                }
                else
                {
                    var rectangle = Screen.FromControl(this).Bounds;
                    this.ParentForm.Location = new Point((rectangle.Width - OldSize.Width) / 2, (rectangle.Height - OldSize.Height) / 2);
                }
                this.maximizeBtn.Image = global::Menporul.Windows.Controls.MenResource.maximize;
            }
            else
            {
                IsMinimize = false;
                CurrentWindowsState = MenWindowsState.Maximized;
                var curWork = this.CurrentCursorWorkingArea;
                this.ParentForm.Location = new Point(curWork.X, curWork.Y);
                this.ParentForm.Size = new Size(curWork.Width, curWork.Height);
                this.maximizeBtn.Image = global::Menporul.Windows.Controls.MenResource.restore;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #endregion

        #region Control box Mouseover/Mouse Leave Color
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBarBtn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            formToolTip.Hide(btn);
            btn.BackColor = MenConstants.TitleBarBtnBackColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBarBtn_MouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var btnName = btn.Name;
            btn.BackColor = MenConstants.TitleBarBtnMouseHoverColor;
            string toolTipText = string.Empty;
            if (btnName == "minimizeBtn")
            {
                toolTipText = "Minimize";
            }
            else if (btnName == "maximizeBtn")
            {
                if (CurrentWindowsState == MenWindowsState.Maximized)
                {
                    toolTipText = "Restore down";
                }
                else
                {
                    toolTipText = "Maximize";
                }
            }
            else if (btnName == "closeBtn")
            {
                toolTipText = "Close";
            }
            Point cursorPosition = btn.Location;
            formToolTip.Show(toolTipText, btn, 10, 30);
        }

        #endregion

        #region Windows Title bar events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBarPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dockFlag = !dockFlag;
            if (e.Button == MouseButtons.Left && EnableMinMax)
            {
                if (e.Clicks >= 2)
                {
                    maximizeBtn_Click(sender, e);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleBarPanel_Resize(object sender, EventArgs e)
        {
            if (this.EnableMinMax)
            {
                this.appTitlelbl.Width = (this.minimizeBtn.Location.X - this.appTitlelbl.Location.X);
            }
            else
            {
                this.appTitlelbl.Width = (this.closeBtn.Location.X - this.appTitlelbl.Location.X);
            }
        }
        #endregion

        #region Native Method
        /// <summary>
        /// Remove the Control Events .And make it has Transparency.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MenConstants.WM_NCHITTEST)
            {
                m.Result = new IntPtr(-1);
                return;
            }
            base.WndProc(ref m);
        }
        #endregion
    }
}
