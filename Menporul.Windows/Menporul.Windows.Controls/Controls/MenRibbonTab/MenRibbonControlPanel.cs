using System;
using System.Collections.Generic;
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
    public class MenRibbonControlPanel : Panel
    {
        public MenRibbonControlPanel(string footerName)
            : this()
        {
            this.FooterLbl.Text = footerName;
        }
        public MenRibbonControlPanel()
        {
            this.VSeparator = new MenVerticalSeparater();
            this.FooterPanel = new Panel();
            this.FooterLbl = new Label();
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.Location = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.Y);
            this.Visible = true;
            this.Size = new System.Drawing.Size(MenRibbonConstants.ControlPanelWidth, MenRibbonConstants.ControlPanelHeight);
            this.TabIndex = 0;

            //
            //VSeparator
            //
            this.VSeparator.Dock = System.Windows.Forms.DockStyle.Right;
            this.VSeparator.Name = "MenVerticalSeparator";

            // 
            // FooterLbl
            // 
            this.FooterLbl.Name = "FooterLbl";
            this.FooterLbl.Text = "";
            this.FooterLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FooterLbl.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);

            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.FooterLbl);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(55, MenRibbonConstants.FooterPanelHeight);
            this.FooterPanel.TabIndex = 0;
            this.FooterPanel.Margin = new Padding(2, 2, 2, 2);

            this.Controls.Add(FooterPanel);
            this.Controls.Add(this.VSeparator);

        }
        public bool AlignControls()
        {

            bool IsAnyVisible = false;
            System.Drawing.Point NewLocation = new System.Drawing.Point(MenRibbonConstants.X, MenRibbonConstants.Y);
            int largeCount = 0;
            int largeTextCount = 0;
            int largeImageTextCount = 0;
            int j = 1;
            //int newHeight = 0;
            int compactWidth = 0;
            List<int> compactWidthCollections = new List<int>(); ;
            int compactButtonCount = 0;
            //System.Drawing.Graphics g = CreateGraphics();

            foreach (var item in this.Controls.OfType<MenRibbonButton>().Where(o => o.Visible2 == true).OrderBy(o => o.Index))
            {
                IsAnyVisible = true;
                if (item.ButtonStyle == MenRibbonButtonType.Large)
                {
                    largeCount++;
                    if (compactWidth != 0)
                    {
                        item.Location = new System.Drawing.Point(NewLocation.X + compactWidth, 2);
                        compactButtonCount = 0;
                        //compactWidth = 0;
                        j = 0;
                    }
                    else
                    {
                        item.Location = NewLocation;
                    }

                    NewLocation = new System.Drawing.Point(NewLocation.X + MenRibbonConstants.LargeBtnWidth + MenRibbonConstants.SmallMarginBetweenControls, MenRibbonConstants.X);
                }
                else if (item.ButtonStyle == MenRibbonButtonType.LargeText)
                {
                    largeTextCount++;
                    item.Location = NewLocation;
                    NewLocation = new System.Drawing.Point(NewLocation.X + MenRibbonConstants.LargeTextBtnWidth, MenRibbonConstants.X);
                }
                else if (item.ButtonStyle == MenRibbonButtonType.LargeImageText)
                {
                    largeImageTextCount++;
                    item.Location = NewLocation;
                    NewLocation = new System.Drawing.Point(NewLocation.X + MenRibbonConstants.LargeImageTextBtnWidth + MenRibbonConstants.SmallMarginBetweenControls, MenRibbonConstants.X);
                }
                else if (item.ButtonStyle == MenRibbonButtonType.Compact)
                {
                    compactButtonCount++;
                    var tetxWidth = TextRenderer.MeasureText(item.Text, MenRibbonConstants.TextFont).Width;
                    //var tetxWidth = ((int)g.MeasureString(item.Text, MenRibbonConstants.TextFont).Width);
                    item.Size = new System.Drawing.Size(MenRibbonConstants.CompactDefaultWidth + MenRibbonConstants.ControlPanelSpaceWidth + tetxWidth, MenRibbonConstants.CompactBtnHeight);
                    
                    if (compactWidth < item.Width)
                    {
                        compactWidth = item.Width;
                    }
                    item.Location = NewLocation;
                    NewLocation = new System.Drawing.Point(NewLocation.X, j * MenRibbonConstants.CompactBtnHeight);

                    if (compactButtonCount == 3)
                    {
                        compactButtonCount = 0;
                        j = 0;
                        NewLocation = new System.Drawing.Point(NewLocation.X + compactWidth, j * MenRibbonConstants.CompactBtnHeight);
                        compactWidthCollections.Add(compactWidth);
                        compactWidth = 0;
                    }
                    j++;
                }
                this.Size = new System.Drawing.Size((((MenRibbonConstants.LargeBtnWidth + MenRibbonConstants.X) * largeCount) + ((MenRibbonConstants.LargeTextBtnWidth + MenRibbonConstants.X) * largeTextCount) + ((MenRibbonConstants.LargeImageTextBtnWidth + MenRibbonConstants.X) * largeImageTextCount)) + compactWidthCollections.Sum() + compactWidth + MenRibbonConstants.SmallMarginBetweenControls, MenRibbonConstants.ControlPanelHeight);
                this.FooterPanel.Width = this.FooterLbl.Width = this.Size.Width;
            }
            return IsAnyVisible;
        }
        public override string Text
        {
            get { return this.FooterLbl.Text; }
            set { this.FooterLbl.Text = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private MenVerticalSeparater VSeparator;
        /// <summary>
        /// 
        /// </summary>
        private Panel FooterPanel;
        /// <summary>
        /// 
        /// </summary>
        private Label FooterLbl;
    }
}
