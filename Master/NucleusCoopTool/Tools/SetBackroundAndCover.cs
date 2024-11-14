using Nucleus.Gaming;
using Nucleus.Gaming.App.Settings;
using Nucleus.Gaming.Cache;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class SetBackroundAndCover
    {
        private static MainForm mainForm;
        private static int blurValue = App_Misc.Blur;
        private static Color colorTop;
        private static Color colorBottom;


        public static  Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static Bitmap ApplyBlur(Bitmap screenshot)
        {
            if (screenshot == null)
            {
                return mainForm.defBackground;
            }

            GaussianBlur blur = new GaussianBlur(screenshot);

            colorTop = blur.topColor;
            colorBottom = blur.bottomColor;

            screenshot.Dispose();

            return blur.Process(blurValue);
        }

        public static void ApplyBackgroundAndCover(string gameGuid)
        {
            mainForm = MainForm.Instance;

            ///Apply covers
            if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
            {
                mainForm.cover.BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
            }
            else
            {
                mainForm.cover.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "no_cover.png");
            }

            ///Apply screenshots randomly
            if (Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")))
            {
                string[] imgsPath = Directory.GetFiles(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}"));

                if (imgsPath.Length > 0)
                {
                    Random rNum = new Random();
                    int RandomIndex = rNum.Next(0, imgsPath.Length);

                    Bitmap backgroundImg = ApplyBlur(new Bitmap(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}\\{RandomIndex}_{gameGuid}.jpeg")));
                    mainForm.BackgroundImg = backgroundImg;
                    mainForm.GameBorderGradientTop = colorTop;
                    mainForm.GameBorderGradientBottom = colorBottom;
                }
                else
                {
                    Bitmap def = ApplyBlur(new Bitmap((Bitmap)mainForm.defBackground.Clone()));
                    mainForm.BackgroundImg = def;
                    mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                    mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
                }
            }
            else
            {
                Bitmap def = ApplyBlur(new Bitmap((Bitmap)mainForm.defBackground.Clone()));
                mainForm.BackgroundImg = def;
                mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
            }
        }
    }
}
