using Nucleus.Gaming;
using Nucleus.Gaming.App.Settings;
using Nucleus.Gaming.Cache;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class SetBackroundAndCover
    {
        private static MainForm main;
        private static int blurValue = App_Misc.Blur;
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

            screenshot.Dispose();
            return result;
        }

        public static void ApplyBackgroundAndCover(MainForm mainForm, string gameGuid)
        {
            main = mainForm;

            ///Apply covers
            if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
            {
                mainForm.cover.BackgroundImage = (Bitmap)Image.FromFile(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
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

                    mainForm.clientAreaPanel.BackgroundImage = ApplyBlur((Bitmap)Image.FromFile(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}\\{RandomIndex}_{gameGuid}.jpeg")));
                    mainForm.GameBorderGradientTop = colorTop;
                    mainForm.GameBorderGradientBottom = colorBottom;
                }
                else
                {
                    Bitmap def = new Bitmap (mainForm.defBackground.Clone() as Bitmap);
                    mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(def);
                    mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                    mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
                }
            }
            else
            {
                Bitmap def = new Bitmap(mainForm.defBackground.Clone() as Bitmap);
                mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(def);
                mainForm.GameBorderGradientTop = mainForm.BorderGradient;
                mainForm.GameBorderGradientBottom = mainForm.BorderGradient;
            }
        }
    }
}
