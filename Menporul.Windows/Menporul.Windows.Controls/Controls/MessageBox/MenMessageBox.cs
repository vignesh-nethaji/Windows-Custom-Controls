using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Message Box
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenMessageBox
    {
        public static DialogResult Show(string text)
        {            
            return new MenMessageBoxTemplate(text).ShowDialog();
        }
        public static DialogResult Show(string text, string caption)
        {
            MenMessageBoxTemplate MenMessage = new MenMessageBoxTemplate(text, caption);
            return MenMessage.ShowDialog();
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons msgBtns)
        {
           
            MenMessageBoxTemplate MenMessage = new MenMessageBoxTemplate(text, caption, msgBtns);
            return MenMessage.ShowDialog();
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons msgBtns, MessageBoxIcon msgIcon)
        {
            MenMessageBoxTemplate MenMessage = new MenMessageBoxTemplate(text, caption, msgBtns, msgIcon);
            return MenMessage.ShowDialog();
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons msgBtns, MessageBoxIcon msgIcon, MessageBoxDefaultButton msgDefaultBtn)
        {
            MenMessageBoxTemplate MenMessage = new MenMessageBoxTemplate(text, caption, msgBtns, msgIcon, msgDefaultBtn);
            return MenMessage.ShowDialog();
        }
    }


}
