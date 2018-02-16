using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;
namespace Menporul.Windows.Controls
{
	/// <summary> 
	/// MenTreeViewScrollBar is a partial class for extending the MenTreeView. 
	/// This partial class can handle the all ScrollBar Actions
	/// </summary> 
    /// <author>Vignesh Nethaji</author> 

    public partial class MenTreeView
    {
        #region Variables and Events
        /// <summary>
        /// this variable represents the vertical old Value . its defines the last scroll vartical scroll value
        /// </summary>
        private int vOldValue = -1;
        /// <summary>
        /// this variable represents the horizontal old Value . its defines the last scroll horizontal scroll value
        /// </summary>
        private int hOldValue = -1;
        /// <summary>
        ///  this variable represents the vertical new Value . its defines the current scroll vartical scroll value
        /// </summary>
        private int vNewValue = -1;
        /// <summary>
        /// this variable represents the horizontal new Value . its defines the current scroll horizontal scroll value
        /// </summary>
        private int hNewValue = -1;
        /// <summary>
        /// This Handler is used to handle the Scroll 
        /// </summary>
        public event ScrollEventHandler Scroll;

        #endregion

        #region Scroll Methods
        /// <summary>
        /// After Moving the Vertiacal or Horizontal scrollbar then change the old and new value.this methods swap the new and old values.
        /// </summary>
        /// <param name="scrollValue">Current scroll value</param>
        /// <param name="scrollOrient">Current scroll action </param>
        private void SwapScrollValues(int scrollValue, ScrollOrientation scrollOrient)
        {            
            if (scrollOrient == ScrollOrientation.HorizontalScroll)
            {
                if (hNewValue != -1)
                {
                    hOldValue = hNewValue;
                }
                hNewValue = scrollValue;
            }
            else if (scrollOrient == ScrollOrientation.VerticalScroll)
            {
                if (vNewValue != -1)
                {
                    vOldValue = vNewValue;
                }
                vNewValue = scrollValue;
            }
        }

        /// <summary>
        /// this method excute the Scroll Operation based up on scroll event type and Scroll orientation.
        /// </summary>
        /// <param name="scrollEventType"></param>
        /// <param name="scrollOrientation"></param>
        private void ExecuteScroll(ScrollEventType scrollEventType, ScrollOrientation scrollOrientation)
        {
            //Gets the Current Scroll value
            int scrollValue = GetScrollPos(this.Handle, scrollOrientation == ScrollOrientation.VerticalScroll ? ColorConstants.SB_VERT : ColorConstants.SB_HORZ);
            //Check current scroll value not equal to new Value .because of ,sometimes event has fired multiple times
            if (scrollValue != vNewValue && scrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.SwapScrollValues(scrollValue, scrollOrientation);
                this.Scroll(this, new ScrollEventArgs(scrollEventType, vOldValue, vNewValue, scrollOrientation));
            }
            else if (scrollValue != hNewValue && scrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.SwapScrollValues(scrollValue, scrollOrientation);
                this.Scroll(this, new ScrollEventArgs(scrollEventType, hOldValue, hNewValue, scrollOrientation));
            }
        }

        #endregion

        #region ScrollBar Commands
        /// <summary>
        /// 
        /// </summary>
        public enum ScrollBarCommands : int
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (this.Scroll != null)
            {
                switch (m.Msg)
                {
                    case ColorConstants.WM_VSCROLL:
                        this.ExecuteScroll(ScrollEventType.EndScroll, ScrollOrientation.VerticalScroll);
                        break;

                    case ColorConstants.WM_MOUSEWHEEL:
                        this.ExecuteScroll(ScrollEventType.EndScroll, ScrollOrientation.VerticalScroll);
                        break;
                    case ColorConstants.WM_HSCROLL:
                        this.ExecuteScroll(ScrollEventType.EndScroll, ScrollOrientation.HorizontalScroll);
                        break;
                    case ColorConstants.WM_KEYDOWN:
                        switch (m.WParam.ToInt32())
                        {

                            case (int)Keys.PageDown:
                                this.ExecuteScroll(ScrollEventType.LargeDecrement, ScrollOrientation.VerticalScroll);
                                break;
                            case (int)Keys.PageUp:
                                this.ExecuteScroll(ScrollEventType.LargeIncrement, ScrollOrientation.VerticalScroll);
                                break;
                            case (int)Keys.Home:
                                this.ExecuteScroll(ScrollEventType.First, ScrollOrientation.VerticalScroll);
                                break;
                            case (int)Keys.End:
                                this.ExecuteScroll(ScrollEventType.Last, ScrollOrientation.VerticalScroll);
                                break;

                        }
                        break;
                }
            }

        }

        #region Native Methods

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        [DllImport("user32.dll")]
        public static extern int SendMessage(
              int hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg,
                                       int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint wMsg,
                                       IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetScrollPos(IntPtr hWnd, int nBar);


        [StructLayout(LayoutKind.Sequential)]
        struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        #endregion
    }
}
