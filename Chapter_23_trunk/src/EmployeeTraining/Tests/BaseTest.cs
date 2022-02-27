using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class BaseTest {


        #region Protected Methods

        protected bool CompareImages(byte[] imagebytes1, byte[] imagebytes2) {
            MemoryStream ms1 = new MemoryStream(imagebytes1, 0, imagebytes1.Length);
            MemoryStream ms2 = new MemoryStream(imagebytes2, 0, imagebytes2.Length);
            Bitmap image1 = new Bitmap(ms1);
            Bitmap image2 = new Bitmap(ms2);

            bool return_val = true;
            for (int x = 0; x < image1.Width; x++) {
                for (int y = 0; y < image1.Height; y++) {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y)) {
                        return_val = false;
                        return return_val;
                    }
                }
            }

            return return_val;
        }

        #endregion Protected Methods
    } // end BaseTest class definition
} // end namespace
