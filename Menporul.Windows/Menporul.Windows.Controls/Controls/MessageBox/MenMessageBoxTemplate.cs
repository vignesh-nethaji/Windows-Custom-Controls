using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Message Box Template
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public partial class MenMessageBoxTemplate : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private MenMessageBoxTemplate()
        {
            this.InitializeComponent();
            this.BackColor = Color.White;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public MenMessageBoxTemplate(string text) : this()
        {

            this.lblMessage.Text = text;
            //addign the panel for label only 
            this.AddPanels(this.panMessage);
            this.footerBarPanel.Controls.Add(this.btnOK);
            this.AcceptButton = this.btnOK;
            this.FormResize();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public MenMessageBoxTemplate(string text, string caption) : this(text)
        {
            this.Text = caption;
            this.footerBarPanel.Controls.Add(this.btnOK);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="msgBtns"></param>
        public MenMessageBoxTemplate(string text, string caption, MessageBoxButtons msgBtns) : this()
        {
            this.Text = caption;
            this.lblMessage.Text = text;
            this.AddPanels(this.panMessage);
            this.AddFooterButtons(msgBtns);
            this.SetFirstDefaultButton(msgBtns);
            this.FormResize();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="msgBtns"></param>
        /// <param name="msgIcon"></param>
        public MenMessageBoxTemplate(string text, string caption, MessageBoxButtons msgBtns, MessageBoxIcon msgIcon) : this()
        {
            this.Text = caption;
            this.lblMessageWithIcon.Text = text;
            this.AddPanels(this.panMessageWithIcon);
            this.AddFooterButtons(msgBtns);
            this.AddIcon(msgIcon);
            this.SetFirstDefaultButton(msgBtns);
            this.FormResizeWithIcon();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="msgBtns"></param>
        /// <param name="msgIcon"></param>
        /// <param name="msgDefaultBtn"></param>
        public MenMessageBoxTemplate(string text, string caption, MessageBoxButtons msgBtns, MessageBoxIcon msgIcon, MessageBoxDefaultButton msgDefaultBtn) : this()
        {
            this.Text = caption;
            this.lblMessageWithIcon.Text = text;
            this.AddPanels(this.panMessageWithIcon);
            this.AddFooterButtons(msgBtns);
            this.AddIcon(msgIcon);
            this.SetDefaultButton(msgDefaultBtn, msgBtns);
            this.FormResizeWithIcon();
        }
        /// <summary>
        /// 
        /// </summary>
        private void FormResizeWithIcon()
        {

            Size textSize = TextRenderer.MeasureText(this.lblMessageWithIcon.Text, this.lblMessageWithIcon.Font, new Size(this.Width, Int32.MaxValue));
            int count = this.lblMessageWithIcon.Text.Split('\n').Length;
            if (this.MaximumSize.Width >= textSize.Width)
            {
                this.Width = textSize.Width + 60;
                int height =  (textSize.Height * (count == 0 ? 1 : count))  / 2;
                if (this.MinimumSize.Height >=height)
                {
                    height = this.MinimumSize.Height;
                }
                this.Height = height;
                this.lblMessageWithIcon.Size = new Size(textSize.Width,this.Height - 60);
               
            }
            else
            {
                this.Width = this.MaximumSize.Width;
                var height = (textSize.Width / textSize.Height) - 80;
                if (height < 200)
                {
                    height = 200;
                }
                this.Height = height;
                this.lblMessageWithIcon.Size = new Size(this.Width - 64, this.Height -60 );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FormResize()
        {

            Size textSize = TextRenderer.MeasureText(this.lblMessage.Text, this.lblMessage.Font, new Size(this.Width, Int32.MaxValue));

            if (this.MaximumSize.Width >= textSize.Width)
            {
                this.Width = textSize.Width + 60;
                this.lblMessage.Size = new Size(this.Width - 34, this.Height - 60);
            }
            else
            {
                this.Width = this.MaximumSize.Width;
                var height = (textSize.Width / textSize.Height) - 80;
                if (height < 200)
                {
                    height = 200;
                }
                this.Height = height;
                this.lblMessage.Size = new Size(this.Width - 34, textSize.Width / textSize.Height);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgBtns"></param>
        private void AddFooterButtons(MessageBoxButtons msgBtns)
        {
            if (msgBtns == MessageBoxButtons.OKCancel)
            {
                this.footerBarPanel.Controls.Add(this.btnCancel);
                this.footerBarPanel.Controls.Add(this.btnOK);
            }
            else if (msgBtns == MessageBoxButtons.YesNo)
            {
                 this.footerBarPanel.Controls.Add(this.btnNo);
                this.footerBarPanel.Controls.Add(this.btnYes);
            }
            else if (msgBtns == MessageBoxButtons.YesNoCancel)
            {
                this.footerBarPanel.Controls.Add(this.btnCancel);
                this.footerBarPanel.Controls.Add(this.btnNo);
                this.footerBarPanel.Controls.Add(this.btnYes);
            }
            else if (msgBtns == MessageBoxButtons.RetryCancel)
            {
                this.footerBarPanel.Controls.Add(this.btnCancel);
                this.footerBarPanel.Controls.Add(this.btnRetry);
            }
            else
            {
                this.footerBarPanel.Controls.Add(this.btnOK);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgIcon"></param>
        private void AddIcon(MessageBoxIcon msgIcon)
        {
            switch (msgIcon.ToString())
            {
                case "Exclamation":
                    this.pbxMessage.Image = global::Menporul.Windows.Controls.MenResource.MessageBoxExclamation;
                    break;
                case "Warning":
                    this.pbxMessage.Image = global::Menporul.Windows.Controls.MenResource.MessageBoxWarning;
                    break;
                case "Asterisk":
                    this.pbxMessage.Image = global::Menporul.Windows.Controls.MenResource.MessageBoxInformation;
                    break;
                case "Question":
                    this.pbxMessage.Image = global::Menporul.Windows.Controls.MenResource.MessageBoxQuestion;
                    break;
                case "Hand":
                    this.pbxMessage.Image = global::Menporul.Windows.Controls.MenResource.MessageBoxError;
                    break;
                case "None":
                    this.lblMessageWithIcon.Location = this.lblMessage.Location;
                    break;
                default:
                    break;
            }
        }
        public void AddPanels(Panel panel)
        {
            this.formPanel.Controls.Add(this.footerBarPanel);
            this.formPanel.Controls.Add(panel);
            this.formPanel.Controls.Add(this.titleBarPanel);
        }
        private void SetFirstDefaultButton(MessageBoxButtons msgBtns)
        {
            if (msgBtns == MessageBoxButtons.OKCancel)
            {
                this.AcceptButton = this.btnOK;
            }
            else if (msgBtns == MessageBoxButtons.YesNoCancel || msgBtns == MessageBoxButtons.YesNo)
            {
                this.AcceptButton = this.btnYes;
            }
        }
        private void SetDefaultButton(MessageBoxDefaultButton msgDefaultbtn, MessageBoxButtons msgBtns)
        {
            switch (msgDefaultbtn)
            {
                case MessageBoxDefaultButton.Button1:
                    if (msgBtns == MessageBoxButtons.OKCancel)
                    {
                        this.AcceptButton = this.btnOK;
                    }
                    else if (msgBtns == MessageBoxButtons.YesNoCancel)
                    {
                        this.AcceptButton = this.btnYes;
                    }
                    break;
                case MessageBoxDefaultButton.Button2:
                    if (msgBtns == MessageBoxButtons.OKCancel)
                    {
                        this.AcceptButton = this.btnCancel;
                    }
                    else if (msgBtns == MessageBoxButtons.YesNoCancel)
                    {
                        this.AcceptButton = this.btnNo;
                    }
                    break;
                case MessageBoxDefaultButton.Button3:
                    if (msgBtns == MessageBoxButtons.YesNoCancel)
                    {
                        this.AcceptButton = this.btnCancel;
                    }
                    break;
                default:
                    break;
            }

        }
        private void footerBarPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Location = new Point(this.footerBarPanel.Width - (6 + (81 * this.footerBarPanel.Controls.Count)), 3);
        }

        private void MenMessageBoxTemplate_Load(object sender, EventArgs e)
        {
            this.lblFormTitle.Text = this.Text;
        }

        #region Windows Title bar events
        #region Constant for Title bar Events
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCRBBUTTONDOWN = 0xA4;
        private const int WM_POPUPSYSTEMMENU = 0x313;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private bool dockFlag;

        private const int WS_SYSMENU = 0x80000;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_MAXIMIZEBOX = 0x10000;
        #endregion
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.Style = WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
                return p;
            }
        }
        private void titleBarPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks >= 2)
                {
                    //maximizeBtn_Click(sender, e);
                }
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, (IntPtr)0);
            }
            else if (e.Button == MouseButtons.Right)
            {
                var p = MousePosition.X + (MousePosition.Y * 0x10000);
                SendMessage(Handle, WM_POPUPSYSTEMMENU, (IntPtr)0, (IntPtr)p);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                const int resizeArea = 10;
                Point cursorPosition = PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                if (cursorPosition.X >= ClientSize.Width - resizeArea && cursorPosition.Y >= ClientSize.Height - resizeArea)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }

            base.WndProc(ref m);
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
