using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class Splashscreen : Form
    {
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

        private readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        private SoundPlayer splayer;
        private string themePath;

        public void SoundPlayer(string filePath)
        {
            splayer = new SoundPlayer(filePath);
            splayer.Play();
            splayer.Dispose();
        }

        public Splashscreen()
        {
            string ChoosenTheme = ini.IniReadValue("Theme", "Theme");
            IniFile theme = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\theme\\" + ChoosenTheme, "theme.ini"));            
            themePath = Path.Combine(Application.StartupPath, @"gui\theme\" + ChoosenTheme);
            bool roundedcorners = Convert.ToBoolean(theme.IniReadValue("Misc", "UseRoundedCorners"));
            
            InitializeComponent();

            if (roundedcorners)
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            gif.Image = new Bitmap(themePath + "\\splash.gif");          
        }

        private void gif_Click(object sender, EventArgs e)
        {
            splayer.Stop();
            Close();
        }

        private void Splashscreen_Shown(object sender, EventArgs e)
        {
            SoundPlayer(themePath + "\\intro.wav");
        }
    }
}
