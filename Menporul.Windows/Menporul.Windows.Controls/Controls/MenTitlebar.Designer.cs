namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Titlebar 
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    partial class MenTitlebar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleBarPanel = new MenDarkPanel();
            this.appMidTitlelbl = new MenTransparentLabel();
            this.appTitlelbl = new MenTransparentLabel();
            this.appLogo = new System.Windows.Forms.PictureBox();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.titleBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBarPanel
            // 
            this.titleBarPanel.BackColor = System.Drawing.Color.Black;
            this.titleBarPanel.Controls.Add(this.appMidTitlelbl);
            this.titleBarPanel.Controls.Add(this.appTitlelbl);
            this.titleBarPanel.Controls.Add(this.appLogo);
            this.titleBarPanel.Controls.Add(this.minimizeBtn);
            this.titleBarPanel.Controls.Add(this.closeBtn);
            this.titleBarPanel.Controls.Add(this.maximizeBtn);
            this.titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBarPanel.Location = new System.Drawing.Point(0, 0);
            this.titleBarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarPanel.Name = "titleBarPanel";
            this.titleBarPanel.Size = new System.Drawing.Size(1154, 25);
            this.titleBarPanel.TabIndex = 36;
            // 
            // appMidTitlelbl
            // 
            this.appMidTitlelbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.appMidTitlelbl.AutoSize = true;
            this.appMidTitlelbl.ForeColor = System.Drawing.Color.White;
            this.appMidTitlelbl.Location = new System.Drawing.Point(470, 6);
            this.appMidTitlelbl.Name = "appMidTitlelbl";
            this.appMidTitlelbl.Size = new System.Drawing.Size(0, 13);
            this.appMidTitlelbl.TabIndex = 9;
            // 
            // appTitlelbl
            // 
            this.appTitlelbl.AutoEllipsis = true;
            this.appTitlelbl.ForeColor = System.Drawing.Color.White;
            this.appTitlelbl.Location = new System.Drawing.Point(45, 6);
            this.appTitlelbl.Margin = new System.Windows.Forms.Padding(5);
            this.appTitlelbl.Name = "appTitlelbl";
            this.appTitlelbl.Size = new System.Drawing.Size(29, 13);
            this.appTitlelbl.TabIndex = 5;
            this.appTitlelbl.Text = "Menporul";
            this.appTitlelbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBarPanel_MouseDown);
            // 
            // appLogo
            // 
            this.appLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appLogo.Image = global::Menporul.Windows.Controls.MenResource.MenLogo;
            this.appLogo.Location = new System.Drawing.Point(5, 5);
            this.appLogo.Margin = new System.Windows.Forms.Padding(5);
            this.appLogo.MaximumSize = new System.Drawing.Size(37, 15);
            this.appLogo.MinimumSize = new System.Drawing.Size(37, 10);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(37, 15);
            this.appLogo.TabIndex = 4;
            this.appLogo.TabStop = false;
            this.appLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBarPanel_MouseDown);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Image = global::Menporul.Windows.Controls.MenResource.minimize;
            this.minimizeBtn.Location = new System.Drawing.Point(1087, 0);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(25, 22);
            this.minimizeBtn.TabIndex = 7;
            this.minimizeBtn.TabStop = false;
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            this.minimizeBtn.MouseLeave += new System.EventHandler(this.titleBarBtn_MouseLeave);
            this.minimizeBtn.MouseHover += new System.EventHandler(this.titleBarBtn_MouseHover);
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Image = global::Menporul.Windows.Controls.MenResource.close;
            this.closeBtn.Location = new System.Drawing.Point(1131, 0);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(25, 22);
            this.closeBtn.TabIndex = 8;
            this.closeBtn.TabStop = false;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            this.closeBtn.MouseLeave += new System.EventHandler(this.titleBarBtn_MouseLeave);
            this.closeBtn.MouseHover += new System.EventHandler(this.titleBarBtn_MouseHover);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.Image = global::Menporul.Windows.Controls.MenResource.maximize;
            this.maximizeBtn.Location = new System.Drawing.Point(1109, 0);
            this.maximizeBtn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(25, 22);
            this.maximizeBtn.TabIndex = 6;
            this.maximizeBtn.TabStop = false;
            this.maximizeBtn.UseVisualStyleBackColor = false;
            this.maximizeBtn.Click += new System.EventHandler(this.maximizeBtn_Click);
            this.maximizeBtn.MouseLeave += new System.EventHandler(this.titleBarBtn_MouseLeave);
            this.maximizeBtn.MouseHover += new System.EventHandler(this.titleBarBtn_MouseHover);
            // 
            // MenTitlebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.titleBarPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MenTitlebar";
            this.Size = new System.Drawing.Size(1154, 25);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBarPanel_MouseDown);
            this.Resize += new System.EventHandler(this.titleBarPanel_Resize);
            this.titleBarPanel.ResumeLayout(false);
            this.titleBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MenDarkPanel titleBarPanel;
        private MenTransparentLabel appTitlelbl;
        private System.Windows.Forms.PictureBox appLogo;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button maximizeBtn;
        private MenTransparentLabel appMidTitlelbl;
    }
}
