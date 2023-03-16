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

        private static Bitmap ApplyBlur(Bitmap screenshot)
        {
            var blur = new GaussianBlur(screenshot as Bitmap);

            Bitmap result = blur.Process(blurValue);

            if (screenshot == null)
            {
                return main.defBackground;
            }

            main.screenshotImg?.Dispose();

            return result;
        }

        public static void ApplyBackgroundAndCover(MainForm mainForm, string gameGuid)
        {
            main = mainForm;
            blurValue = int.Parse(Globals.ini.IniReadValue("Dev", "Blur"));
            ///Apply covers
            if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
            {
                //mainForm.coverImg = ImageCache.GetImage(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                mainForm.clientAreaPanel.SuspendLayout();
                mainForm.cover.BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));// mainForm.coverImg;
                mainForm.clientAreaPanel.ResumeLayout();
                mainForm.coverFrame.Visible = true;
                mainForm.cover.Visible = true;
            }
            else
            {
                mainForm.cover.BackgroundImage = ImageCache.GetImage(Globals.Theme + "no_cover.png");// new Bitmap(Globals.Theme + "no_cover.png");
                mainForm.cover.Visible = true;
                mainForm.coverFrame.Visible = true;
            }

            //Apply screenshots randomly
            if (Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")))
            {
                string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")));
                Random rNum = new Random();
                int RandomIndex = rNum.Next(0, imgsPath.Count());

                //mainForm.screenshotImg = ImageCache.GetImage(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}\\{RandomIndex}_{gameGuid}.jpeg"));//name(1) => directory name ; name(2) = partial image name 
                mainForm.clientAreaPanel.SuspendLayout();
                mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(ImageCache.GetImage(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}\\{RandomIndex}_{gameGuid}.jpeg")));
                mainForm.clientAreaPanel.ResumeLayout();
            }
            else
            {

                mainForm.clientAreaPanel.SuspendLayout();
                mainForm.clientAreaPanel.BackgroundImage = ApplyBlur(mainForm.defBackground);
                mainForm.clientAreaPanel.ResumeLayout();
            }

            mainForm.btn_textSwitcher.Visible = !mainForm.positionsControl.textZoomContainer.Visible && File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt"));
        }

    }
}
