
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
    /// MenPorul Ribbon Tab Page
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRibbonTabPage : TabPage
    {
        #region Variables declaration
        /// <summary>
        /// Right Scroll Button
        /// </summary>
        MenNormalButton RightScrollBtn;
        /// <summary>
        /// Left Scroll Button
        /// </summary>
        MenNormalButton LeftScrollBtn;
        /// <summary>
        /// Collection is used to store the hidden Controls.
        /// </summary>
        List<MenRibbonControlPanel> panCollection;
        #endregion

        #region TabPage Initilization
        public MenRibbonTabPage()
        {

            this.BackColor = MenConstants.Thin;
            this.panCollection = new List<MenRibbonControlPanel>();

            this.RightScrollBtn = new MenNormalButton();
            this.RightScrollBtn.Size = new Size(15, 70);
            this.RightScrollBtn.BringToFront();
            this.RightScrollBtn.Image = global::Menporul.Windows.Controls.MenResource.Forward;
            this.RightScrollBtn.Dock = DockStyle.Right;
            this.RightScrollBtn.Click += RightScrollBtn_Click;
            this.RightScrollBtn.Visible = true;
            this.Controls.Add(RightScrollBtn);

            this.LeftScrollBtn = new MenNormalButton();
            this.LeftScrollBtn.Size = new Size(15, 70);
            this.LeftScrollBtn.BringToFront();
            this.LeftScrollBtn.Image = global::Menporul.Windows.Controls.MenResource.Backward;
            this.LeftScrollBtn.Dock = DockStyle.Left;
            this.LeftScrollBtn.Visible = true;
            this.LeftScrollBtn.Click += LeftScrollBtn_Click;
        }
        #endregion

        #region Size Changing Event
        /// <summary>
        ///  adding the controls based on the size of tabpage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ControlAdding();
        }
        #endregion

        #region Scroll Buttons

        #region Scroll Button Events
        /// <summary>
        /// Show the last control panel which was hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftScrollBtn_Click(object sender, EventArgs e)
        {
            if (panCollection.Count > 0)
            {
                this.panCollection.Last().Visible = true;
                this.panCollection.RemoveAt(this.panCollection.Count - 1);
            }
            LeftBtnHideShow();
            RightBtnHideShow();
        }
        /// <summary>
        /// Hide the first control in the Ribbon tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightScrollBtn_Click(object sender, EventArgs e)
        {
            int itemCount = 0;
            foreach (MenRibbonControlPanel item in this.Controls.OfType<MenRibbonControlPanel>().Reverse())
            {
                itemCount++;
                if (IsCompleteChildVisible(item) && item.Visible == true || item.Name == "")
                {
                    item.Visible = false;
                    panCollection.Add(item);
                    break;
                }
            }
            LeftBtnHideShow();
            RightBtnHideShow();
        }
        #endregion

        #region Scroll buttons Hide and Show
        /// <summary>
        /// Hide the Left scroll button if the first controlpanel visible is true or else show.
        /// </summary>
        private void LeftBtnHideShow()
        {
            this.LeftScrollBtn.Visible = !IsFirstPanelVisible();
        }
        /// <summary>
        /// The Last visibled controlpanel has clearly viewed to user then it's to be Hide or else Show .
        /// </summary>
        private void RightBtnHideShow()
        {
            this.RightScrollBtn.Visible = !IsLastPanelVisible();
        }

        #endregion

        #endregion

        #region Panel Visbile Methods
        /// <summary>
        /// Checks whether the Last Control is Completely visible to user.
        /// </summary>
        /// <returns></returns>
        private bool IsLastPanelVisible()
        {
            MenRibbonControlPanel lastControl = this.Controls.OfType<MenRibbonControlPanel>().Where(o => o.Visible).FirstOrDefault();
            if (this.Controls.OfType<MenRibbonControlPanel>().Count() == 1 || (lastControl != null && lastControl.Visible == true && IsCompleteChildVisible(lastControl)))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Checks the first Panel is Visible to user .
        /// </summary>
        /// <returns></returns>
        private bool IsFirstPanelVisible()
        {
            MenRibbonControlPanel lastControl = this.Controls.OfType<MenRibbonControlPanel>().LastOrDefault();
            if (this.Controls.OfType<MenRibbonControlPanel>().Count() == 1 || (lastControl.Visible == true && IsCompleteChildVisible(lastControl)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks whether the control Panel has completely visible to user 
        /// </summary>
        /// <param name="child">Control Panel has Argument</param>
        /// <returns></returns>
        public bool IsCompleteChildVisible(Control child)
        {
            //bool result = false;
            var pos = this.PointToClient(child.PointToScreen(Point.Empty));
            if (this.GetChildAtPoint(pos) == child)
            {
                if (this.Width < (child.Location.X + child.Width))
                {
                    return false;
                }
                return true;
            }
            if ((this.GetChildAtPoint(new Point(pos.X + child.Width - 1, pos.Y)) == child))
            {
                return true;
            }
            if (this.GetChildAtPoint(new Point(pos.X, pos.Y + child.Height - 1)) == child)
            {
                return true;
            }
            if (this.GetChildAtPoint(new Point(pos.X + child.Width - 1, pos.Y + child.Height - 1)) == child)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Control Visibility on Resize/TabChange
        /// <summary>
        ///  if any control was hidden then it Enables the control panel visible based on the screen size.
        /// </summary>
        public void ControlAdding()
        {
            if (this.panCollection.Count > 0)
            {
                var lastControl = this.Controls.OfType<MenRibbonControlPanel>().Where(o => o.Visible).FirstOrDefault();
                var tempControl = this.panCollection.LastOrDefault();
                while (lastControl != null && tempControl != null && (this.Width > (lastControl.Location.X + lastControl.Width + tempControl.Width)) && this.panCollection.Count > 0)
                {
                    tempControl.Visible = true;
                    this.panCollection.Remove(tempControl);
                    tempControl = this.panCollection.LastOrDefault();
                }
            }
            LeftBtnHideShow();
            RightBtnHideShow();
        }
        #endregion

        #region ControlPanel Panel Alignment
        /// <summary>
        /// Align all control panels based on the page size.
        /// </summary>
        public void AlignControls()
        {
            foreach (var item in this.Controls.OfType<MenRibbonControlPanel>())
            {
                item.Visible = item.AlignControls();
            }
            this.Controls.Add(LeftScrollBtn);
        }
        #endregion
    }
}
