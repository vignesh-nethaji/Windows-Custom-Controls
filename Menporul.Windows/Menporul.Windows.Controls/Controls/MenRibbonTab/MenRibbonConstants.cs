using System;
using System.Collections.Generic;
using System.Linq;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// MenPorul Ribbon Constants
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRibbonConstants
    {
        public readonly static System.Drawing.Font TextFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public const int X = 2;
        public const int Y = 2;
        public const int CompactBtnHeight = 22;
        public const int CompactPrimaryImageSize = 16;
        public const int CompactPrimarySpace = CompactPrimaryImageSize + 8;
        public const int CompactChildSpace = ChildImageSize + 8;
        public const int CompactDefaultWidth = CompactPrimarySpace + CompactChildSpace;
        public const int CompactMoreImageHeight = 8;
        public const int CompactMoreImageWidth = 8;
        public const int CompactChildImageY = 7;

        public const int BigMarginBetweenControl = 5;
        public const int MarginBetweenCompactControls = 2;
        public const int SmallMarginBetweenControls = 2;

        public const int LargeBtnHeight = 72;
        public const int LargeBtnWidth = 84;
        public const int LargePrimaryImageSize = 32;
        public const int LargeNoChildTitleMaxSize = LargeBtnWidth;
        public const int LargeChildTitleMaxSize = LargeBtnHeight;
        public const int LargeChildImageX = 68;
        public const int LargeChildImageY = 10;
        public const int LargePrimaryImageX = (LargeBtnWidth- LargePrimaryImageSize) / 2;
        public const int LargeTitleY = LargeBtnWidth / 2;

        public const int LargeTextBtnWidth = 120;
        public const int LargeTextNoChildTitleMaxSize = LargeTextBtnWidth;
        public const int LargeTextChildTitleMaxSize = 108;
        public const int LargeTextPrimaryImageX = (LargeTextBtnWidth - LargePrimaryImageSize) / 2;

        public const int LargeImageTextBtnWidth = 168;
        public const int LargeImageTextNoChildTitleMaxSize = LargeImageTextBtnWidth;
        public const int LargeImageTextChildTitleMaxSize = 156;
        public const int LargeImageTextPrimaryImageSize = 88;
        public const int LargeImageTextPrimaryImageX = (LargeImageTextBtnWidth - LargeImageTextPrimaryImageSize) / 2;
        public const int LargeImageTextTitleY = LargeImageTextBtnWidth / 2;

        public const int ChildImageSize = 8;

        public const int ControlPanelHeight = 96;
        public const int ControlPanelWidth = 86;
        public const int ControlPanelSpaceWidth = 10;

        public const int FooterPanelHeight = 16;

    }
}
