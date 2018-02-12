using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menporul.Windows.Controls.Core
{
    /// <summary>
    /// Menporul ProfessionalColorTable
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    internal class MenToolStripColors : ProfessionalColorTable
    {
        public override Color MenuItemSelectedGradientBegin
        {

            get { return MenConstants.ActiveBackGround; }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return MenConstants.ActiveBackGround; }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return MenConstants.Light;
            }
        }
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return MenConstants.Thin;
            }
        }
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return MenConstants.Thin;
            }
        }
        public override Color ImageMarginGradientEnd
        {
            get
            {
                return MenConstants.Thin;
            }
        }
        public override Color ToolStripBorder
        {
            get
            {
                return MenConstants.Thin;
            }
        }        
        public override Color ToolStripGradientEnd
        {
            get
            {
                return MenConstants.Thin;
            }
        }
        public override Color ToolStripGradientMiddle
        {
            get
            {
                return MenConstants.Thin;
            }
        }
        public override Color ButtonSelectedBorder
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonSelectedHighlight
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonPressedBorder
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }

        public override Color ButtonPressedGradientBegin
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonPressedGradientEnd
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonPressedHighlight
        {
            get
            {
                return MenConstants.ActiveBackGround;
            }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get
            {
                return MenConstants.ActiveBackGround;                
            }
        }
       
    }
}
