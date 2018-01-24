using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Menporul.Windows.Controls.Core
{
    public class MenConstants
    {
        
        #region Windows Message ID's
        /// <summary>
        /// Form Caption area or Titlebar Area
        /// </summary>
        public const int HT_CAPTION = 0x2;
        /// <summary>
        /// Control Transparency Message ID 
        /// </summary>
        public const int WM_NCHITTEST = 0x84;
        /// <summary>
        /// Client Area Left Double Click Message ID
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x0203;//client area 
        /// <summary>
        ///  Client Non Area Left Double Click Message ID
        /// </summary>
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;//non-client area
        /// <summary>
        ///  Client Form Left End Drag Arrow Message ID
        /// </summary>
        public const int HT_LEFT = 10;
        /// <summary>
        ///  Client Form Right End Drag Arrow Message ID
        /// </summary>
        public const int HT_RIGHT = 11;
        /// <summary>
        ///  Client Form Top End Drag Arrow Message ID
        /// </summary>
        public const int HT_TOP = 12;
        /// <summary>
        ///  Client Form Bottom End Drag Arrow Message ID
        /// </summary>
        public const int HT_BOTTOM = 15;
        /// <summary>
        ///  Client Form TopLeft Diagonal Drag Arrow Message ID
        /// </summary>
        public const int HT_TOPLEFT = 13;
        /// <summary>
        ///  Client Form TopRight Diagonal Drag Arrow Message ID
        /// </summary>
        public const int HT_TOPRIGHT = 14;
        /// <summary>
        ///  Client Form BottomLeft Diagonal Drag Arrow Message ID
        /// </summary>
        public const int HT_BOTTOMLEFT = 16;
        /// <summary>
        /// Client Form BottomLeft Diagonal Drag Arrow Message ID
        /// </summary>
        public const int HT_BOTTOMRIGHT = 17;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_NCPAINT = 0x85;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_PAINT = 0x000F;

        #endregion

        #region Application Fonts
        /// <summary>
        /// Application  Primary Font
        /// </summary>
        public static Font AppPrimaryFont = new Font("Arial", 8, FontStyle.Regular);
        /// <summary>
        /// Application  Secondary Font
        /// </summary>
        public static Font AppSecondaryFont = new Font("Arial", 12, FontStyle.Regular);
        /// <summary>
        /// Application  AppBoldFont
        /// </summary>
        public static Font AppBoldFont = new Font("Arial", 12, FontStyle.Bold);
        #endregion

        #region Application Colors

        #region Primary Colors
        /// <summary>
        /// 
        /// <summary>
        public static readonly Color Red = ColorTranslator.FromHtml("#F03040");
        /// <summary>
        /// Constant for Dark Background Color.
        /// </summary>
        public static readonly Color Dark = ColorTranslator.FromHtml("#000000");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color Thin = ColorTranslator.FromHtml("#F0F0F0");
        /// <summary>
        /// Constant for Pure White Color for set Application Text Foreground
        /// </summary>
        public static readonly Color Light = ColorTranslator.FromHtml("#FFFFFF");
        /// <summary>
        /// Constant for Active Blue for Highlight
        /// </summary>
        public static readonly Color HighLightBlue = ColorTranslator.FromHtml("#2E92FA");
        /// <summary>
        /// 
        /// </summary>
        public static Color LightGrey = ColorTranslator.FromHtml("#D2D2D2");
        /// <summary>
        /// Constant for FormBackGround Color has White
        /// </summary>
        public static readonly Color FormBackGround = Light;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ActiveBackGround = LightGrey;
        #endregion

        /// <summary>
        /// Constant for Vertical Separator Color 
        /// </summary>
        public static readonly Color VerticalSeparator = ColorTranslator.FromHtml("#B4B4B4");

        #region TitleBar Colors
        /// <summary>
        /// Constant for Titlebar Button color
        /// </summary>
        public static readonly Color TitleBarBtnBackColor = Dark;
        /// <summary>
        /// Constant for Title bar MouseOver color
        /// </summary>
        public static readonly Color TitleBarBtnMouseHoverColor = ColorTranslator.FromHtml("#222222");
        #endregion

        #region Treeview Colors
        //TreeView [File View]
        /// <summary>
        /// Constant for set the background color on treeview for selected node.
        /// </summary>
        public static readonly Color TreeViewSelectedBackGround = ColorTranslator.FromHtml("#DDEDFD");
        /// <summary>
        /// Constant for set the background color on treeview for selected node.
        /// </summary>
        public static readonly Color TreeViewSelectedDarkBackGround = HighLightBlue;
        /// <summary>
        /// Constant for TreeView node text Color
        /// </summary>
        public static readonly Color TreeViewText = Dark;
        /// <summary>
        /// Constant for TreeView node text Color
        /// </summary>
        public static readonly Color TreeViewSelectedText = Light;

        //MenTreeViewCheckBox
        ///<summary>
        /// Constant for Treeview Checkbox border color
        ///</summary>
        public static readonly Color TreeViewCheckBoxBorder = ColorTranslator.FromHtml("#B4B4B4");
        //MenTreeView
        /// <summary>
        /// MenTreeView left background Color
        /// </summary>
        public static readonly Color MenTreeViewBackground = Light;
        #endregion

        #region Men Normal Button Colors
        ///MenNormalButton
        /// <summary>
        /// Men Normal button Border Color
        /// </summary>
        public static readonly Color BtnNormalBorder = ColorTranslator.FromHtml("#C8C8C8");
        /// <summary>
        /// Men Normal button BottomBorder Color
        /// </summary>
        public static readonly Color BtnNormalBottomBorder = ColorTranslator.FromHtml("#A0A0A0");
        /// <summary>
        /// Men Normal button HOverBorder Color
        /// </summary>
        public static readonly Color BtnNormalHOverBorder = ColorTranslator.FromHtml("#998C8C8C");
        /// <summary>
        /// Men Normal button ActiveBorder Color
        /// </summary>
        public static readonly Color BtnNormalActiveBorder = HighLightBlue;
        /// <summary>
        /// Men Normal button BackGround Color
        /// </summary>
        public static readonly Color BtnNormalBackGround = ColorTranslator.FromHtml("#DCDCDC");
        /// <summary>
        /// Men Normal button Fore Color
        /// </summary>
        public static readonly Color BtnNormalText = ColorTranslator.FromHtml("#464646");
        /// <summary>
        /// Men Normal button MouseDownBack Color
        /// </summary>
        public static readonly Color BtnMouseDownBackGround = ColorTranslator.FromHtml("#DEDEDE");
        /// <summary>
        /// Men Normal button MouseOverBack Color
        /// </summary>
        public static readonly Color BtnMouseOverBackGround = ColorTranslator.FromHtml("#FAFAFA");

        #endregion

        #region Men Primary Button Colors
        //Men Primary Button
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryBorder = ColorTranslator.FromHtml("#0C74DA");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryBackGround = ColorTranslator.FromHtml("#0C74DA");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryBottomBorder = ColorTranslator.FromHtml("#004C97");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryTextColor = Light;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryHOverBorder = HighLightBlue;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryHOverBack = HighLightBlue;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryActiveBorder = ColorTranslator.FromHtml("#004C97");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryActive = ColorTranslator.FromHtml("#004C97");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryDisabledBack = ColorTranslator.FromHtml("#260C74DA");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryDisabledBorder = ColorTranslator.FromHtml("#260C74DA");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color BtnPrimaryDisabledText = ColorTranslator.FromHtml("#80004C97");
        #endregion

        #region Progress Bar Color
        /// <summary>
        /// Progress bar 
        /// </summary>
        public static readonly Color ProgressBarPercentage = HighLightBlue;
        #endregion

        #region Context Menus Colors
        //Context Menus
        /// <summary>
        /// Menu Background 
        /// </summary>
        public static readonly Color ContextMenuBackGround = Light;
        /// <summary>
        /// Menu Foreground
        /// </summary>
        public static readonly Color ContextMenuText = Dark;
        /// <summary>
        /// Menu Seaprator
        /// </summary>
        public static readonly Color ContextMenuSeparator = LightGrey;
        /// <summary>
        /// Menu Border
        /// </summary>
        public static readonly Color ContextMenuBorder = LightGrey;
        /// <summary>
        /// Menu Selected
        /// </summary>
        public static readonly Color ContextMenuSelected = LightGrey;
        #endregion

        #region Men TextBox Colors
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color TxtFocusBorder = HighLightBlue;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color TxtDefaultBorder = LightGrey;
        #endregion

        #region Men Combobox Colors
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color CbxFocusBorder = HighLightBlue;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color CbxDefaultBorder = LightGrey;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color CbxHighLight = HighLightBlue;
        #endregion

        #region Men CheckBox Colors
        //MenCheckbox
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDefaultBorder = ColorTranslator.FromHtml("#CCB4B4B4");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDisabledBorder = ColorTranslator.FromHtml("#66B4B4B4");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkHoverBorder = ColorTranslator.FromHtml("#CC5A5A5A");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDefaultBackGround = ColorTranslator.FromHtml("#CCFFFFFF");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDisabledBackGround = ColorTranslator.FromHtml("#66FFFFFF");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDefaultText = ColorTranslator.FromHtml("#464646");
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkDisabledText = ColorTranslator.FromHtml("#80464646");
        #endregion

        #region Men Checked Listbox Color
        //MenCheckedListbox
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ChkLstCheckBoxBorder = ColorTranslator.FromHtml("#B4B4B4");
        #endregion

        #region Men ToolTip Colors
        // Tooltip
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ToolTipBackGround = Light;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ToolTipBorder = LightGrey;
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color ToolTipText = Dark;
        #endregion

        #endregion

    }
}
