using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class SetBackroundAndCover
    {
        private static MainForm main;
        private static int blurValue;
        private static Color colorTop;
        private static Color colorBottom;

        private static Bitmap ApplyBlur(Bitmap screenshot)
        {
            if (screenshot == null)
            {
                return main.defBackground;
            }

            var blur = new GaussianBlur(screenshot);

            Bitmap result = blur.Process(blurValue);
            colorTop = blur.topColor;
            colorBottom = blur.bottomColor;

            return result;
        }

        public static void ApplyBackgroundAndCover(MainForm mainForm, string gameGuid)
        {
            main = mainForm;
            blurValue = int.Parse(Globals.ini.IniReadValue("Dev", "Blur"));

            ///Apply covers
            if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
            {   
                mainForm.coverImg = new Bitmap(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                mainForm.cover.BackgroundImage = mainForm.coverImg;
            }
            else
            {
                mainForm.cover.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "no_cover.png");
            }

            ///Apply screenshots randomly
            if (Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")))
            {
                string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")));
               
                if (imgsPath.Length > 0)
                {
                    Random rNum = new Random();
                    int RandomIndex = rNum.Next(0, imgsPath.Length);

                    mainForm.screenshotImg = new Bitmap(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}\\{RandomIndex}_{gameGuid}.jpeg"));
                    mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(mainForm.screenshotImg); //name(1) => directory name ; name(2) = partial image path 
                    mainForm.GameBorderGradientTop = colorTop;
                    mainForm.GameBorderGradientBottom = colorBottom;
                }
                else
                {
                    mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(mainForm.defBackground);
                    mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                    mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
                }
            }
            else
            {
                mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(mainForm.defBackground);
                mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
            }

            mainForm.btn_textSwitcher.Visible = !mainForm.setupScreen.textZoomContainer.Visible && File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt"));       
        }
    }
}
