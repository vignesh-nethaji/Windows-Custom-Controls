namespace Menporul.Windows.Controls
{
    /// <summary>
    /// menporul Base Form Designer
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    partial class MenBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenTitlebar1 = new MenTitlebar();
            this.leftResizeSplitter = new MenFormBorderSplitter();
            this.rightResizeSplitter = new MenFormBorderSplitter();
            this.topResizeSplitter = new MenFormBorderSplitter();
            this.bottomResizeSplitter = new MenFormBorderSplitter();
            this.SuspendLayout();
            // 
            // MenTitlebar1
            // 
            this.MenTitlebar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenTitlebar1.EnableMinMax = false;
            this.MenTitlebar1.Location = new System.Drawing.Point(3, 3);
            this.MenTitlebar1.Margin = new System.Windows.Forms.Padding(0);
            this.MenTitlebar1.Name = "MenTitlebar1";
            this.MenTitlebar1.Size = new System.Drawing.Size(824, 25);
            this.MenTitlebar1.TabIndex = 0;
            this.MenTitlebar1.Title = "Menporul";
            // 
            // leftResizeSplitter
            // 
            this.leftResizeSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.leftResizeSplitter.Location = new System.Drawing.Point(0, 0);
            this.leftResizeSplitter.Name = "leftResizeSplitter";
            this.leftResizeSplitter.Size = new System.Drawing.Size(3, 568);
            this.leftResizeSplitter.TabIndex = 1;
            this.leftResizeSplitter.TabStop = false;
            // 
            // rightResizeSplitter
            // 
            this.rightResizeSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.rightResizeSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightResizeSplitter.Location = new System.Drawing.Point(827, 0);
            this.rightResizeSplitter.Name = "rightResizeSplitter";
            this.rightResizeSplitter.Size = new System.Drawing.Size(3, 568);
            this.rightResizeSplitter.TabIndex = 2;
            this.rightResizeSplitter.TabStop = false;
            // 
            // topResizeSplitter
            // 
            this.topResizeSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.topResizeSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.topResizeSplitter.Location = new System.Drawing.Point(3, 0);
            this.topResizeSplitter.Name = "topResizeSplitter";
            this.topResizeSplitter.Size = new System.Drawing.Size(824, 3);
            this.topResizeSplitter.TabIndex = 3;
            this.topResizeSplitter.TabStop = false;
            // 
            // bottomResizeSplitter
            // 
            this.bottomResizeSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bottomResizeSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomResizeSplitter.Location = new System.Drawing.Point(3, 565);
            this.bottomResizeSplitter.Name = "bottomResizeSplitter";
            this.bottomResizeSplitter.Size = new System.Drawing.Size(824, 3);
            this.bottomResizeSplitter.TabIndex = 4;
            this.bottomResizeSplitter.TabStop = false;
            // 
            // MenBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 568);
            this.Controls.Add(this.topResizeSplitter);
            this.Controls.Add(this.MenTitlebar1);
            this.Controls.Add(this.bottomResizeSplitter);            
            this.Controls.Add(this.rightResizeSplitter);
            this.Controls.Add(this.leftResizeSplitter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenBaseForm";
            this.Text = "MenBaseForm";
            this.Load += new System.EventHandler(this.MenBaseForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MenBaseForm_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.MenBaseForm_LocationChanged);
            this.Resize += new System.EventHandler(this.MenBaseForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private MenTitlebar MenTitlebar1;
        private MenFormBorderSplitter leftResizeSplitter;
        private MenFormBorderSplitter rightResizeSplitter;
        private MenFormBorderSplitter topResizeSplitter;
        private MenFormBorderSplitter bottomResizeSplitter;
    }
}