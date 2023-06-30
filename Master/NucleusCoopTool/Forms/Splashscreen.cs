using Nucleus.Gaming;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class Splashscreen : Form
    {
        string theme = Globals.Theme;
        private System.Windows.Forms.Timer DisposeTimer;//dispose splash screen timer
        private SoundPlayer splayer;

        public void SoundPlayer(string filePath)
        {
            splayer = new SoundPlayer(filePath);
            splayer.Play();
            splayer.Dispose();
        }

        public Splashscreen(Size size,Point location,bool roundedcorners)
        {
            InitializeComponent();

            Size = size;
            Location = location;

            if (roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            gif.Image = new Bitmap(theme + "splash.gif");
        }

        private void gif_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void Splashscreen_Shown(object sender, EventArgs e)
        {
            DisposeTimer = new System.Windows.Forms.Timer();
            DisposeTimer.Interval = (2500); //millisecond
            DisposeTimer.Tick += new EventHandler(MainTimerTick);
            DisposeTimer.Start();
        }

        private void MainTimerTick(Object Object, EventArgs EventArgs)
        {
           DisposeTimer.Dispose();
           Close();
        }
    }
}
