using Nucleus.Gaming;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class Splashscreen : Form
    {
        IniFile themeIni = Globals.ThemeIni;
        string theme = Globals.Theme;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
           (
              int nLeftRect,     // x-coordinate of upper-left corner
              int nTopRect,      // y-coordinate of upper-left corner
              int nRightRect,    // x-coordinate of lower-right corner
              int nBottomRect,   // y-coordinate of lower-right corner
              int nWidthEllipse, // width of ellipse
              int nHeightEllipse // height of ellipse
           );

        private SoundPlayer splayer;

        public void SoundPlayer(string filePath)
        {
            splayer = new SoundPlayer(filePath);
            splayer.Play();
            splayer.Dispose();
        }

        public Splashscreen()
        {
            bool roundedcorners = Convert.ToBoolean(themeIni.IniReadValue("Misc", "UseRoundedCorners"));

            InitializeComponent();

            if (Globals.ini.IniReadValue("Misc", "WindowSize") != "")
            {
                string[] windowSize = Globals.ini.IniReadValue("Misc", "WindowSize").Split('X');
                Size = new Size(int.Parse(windowSize[0]), int.Parse(windowSize[1]));
            }

            Rectangle area = Screen.PrimaryScreen.Bounds;
            if (Globals.ini.IniReadValue("Misc", "WindowLocation") != "")
            {
                string[] windowLocation = Globals.ini.IniReadValue("Misc", "WindowLocation").Split('X');
                Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
            }
            else
            {
                CenterToScreen();
            }

            if (roundedcorners)
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            gif.Image = new Bitmap(theme + "splash.gif");
        }

        private void gif_Click(object sender, EventArgs e)
        {
            splayer.Stop();
            Close();
        }

        private void Splashscreen_Shown(object sender, EventArgs e)
        {
            SoundPlayer(theme + "intro.wav");
        }
    }
}
