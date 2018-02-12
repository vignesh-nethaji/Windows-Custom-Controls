using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menporul.Windows.Controls.Core;

namespace Menporul.Windows.Controls
{
    /// <summary>
    /// Menporul Ribbon Picture Box 
    /// </summary>
    /// <author>Vignesh Nethaji</author>
    public class MenRibbonPictureBox : PictureBox
    {
        /// <summary>
        /// 
        /// </summary>
        public MenRibbonPictureBox()
        {
            this.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public MenRibbonPictureBox(int size)
            : this()
        {
            this.Size = new System.Drawing.Size(size, size);
        }
        /// <summary>
        /// 
        /// </summary>
        private void ImageConvert()
        {
            if (!Enabled && Image != null)
            {


                //read image
                Bitmap bmp = (Bitmap)Image.Clone();

                //get image dimension
                int width = bmp.Width;
                int height = bmp.Height;

                //color of pixel
                Color p;

                //grayscale
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //get pixel value
                        p = bmp.GetPixel(x, y);

                        //extract pixel component A
                        int a = p.A;

                        //set new pixel value
                        bmp.SetPixel(x, y, Color.FromArgb(a, 155, 155, 155));
                    }
                }
                //load grayscale image in picturebox2
                base.Image = bmp;
            }
            else
            {
                base.Image = this.Image;
            }
        }       
        /// <summary>
        /// 
        /// </summary>
        private bool _enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {

                base.Enabled = value;
                this._enabled = value;
                ImageConvert();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Image _image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public new Image Image
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
                ImageConvert();
            }
        }
    }
}
