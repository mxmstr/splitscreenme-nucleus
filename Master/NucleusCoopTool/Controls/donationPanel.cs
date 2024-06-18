using Nucleus.Gaming;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.Controls
{
    public partial class DonationPanel : UserControl
    {
        public DonationPanel()
        {
            InitializeComponent();

            BackColor = Color.FromArgb(190, 0, 0, 0);
            btn_credits.FlatAppearance.MouseOverBackColor = Color.Transparent;

            Cursor cursor = new Cursor(Globals.ThemeFolder + "cursor_hand.ico");

            Ilyaki_label.Cursor = cursor;
            Mikou_label.Cursor = cursor;
            Talos91_label.Cursor = cursor;
            btn_credits.Cursor = cursor;

            System.Drawing.Drawing2D.GraphicsPath pict1 = new System.Drawing.Drawing2D.GraphicsPath();
            pict1.AddEllipse(0, 0, image1.Width, image1.Height);
            Region region1 = new Region(pict1);
            image1.Region = region1;

            System.Drawing.Drawing2D.GraphicsPath pict2 = new System.Drawing.Drawing2D.GraphicsPath();    
            pict2.AddEllipse(0, 0, image2.Width, image2.Height);
            Region region2 = new Region(pict2);
            image2.Region = region2;

            System.Drawing.Drawing2D.GraphicsPath pict3 = new System.Drawing.Drawing2D.GraphicsPath();
            pict3.AddEllipse(0, 0, image3.Width, image3.Height);
            Region region3 = new Region(pict3);
            image3.Region = region3;
        }

        private void Ilyaki_label_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/donate/?cmd=_s-xclick&hosted_button_id=HH97DSPF3MPKQ&source=url");
        }

        private void Mikou_label_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/donate/?hosted_button_id=P3NVBYRQ4Z45L");
        }

        private void Talos91_label_Click(object sender, EventArgs e)
        {
            Process.Start("https://ko-fi.com/talos91");
        }

        private void Btn_credits_Click(object sender, EventArgs e)
        {
            string text =
            "Nucleus Co-op - " + Globals.Version +
            "\n " +
            "\nOriginal Nucleus Co-op Project: Lucas Assis(lucasassislar)" +
            "\nNew Nucleus Co-op fork: ZeroFox" +
            "\nMultiple keyboards / mice & hooks: Ilyaki" +
            "\nWebsite & handler API: r - mach" +
            "\nNew UI design, bug fixes, per game profiles and gamepad UI control/shortcuts support : Mikou27(nene27)" +
            "\nHandlers development & testing: Talos91, PoundlandBacon, Pizzo, dr.oldboi and many more." +
            "\nThis new & improved Nucleus Co-op brings a ton of enhancements, such as:" +
            "\n-Massive increase to the amount of compatible games, 400 + as of now." +
            "\n-Beautiful new overhauled user interface with support for themes, game covers & screenshots." +
            "\n-Support for per-game profiles." +
            "\n-Many quality of life improvements & bug fixes." +
            "\n-And so much more!\n" +
            "\nSpecial thanks to: Talos91, dr.oldboi, PoundlandBacon, Pizzo and the rest of the Splitscreen Dreams discord community.";
            NucleusMessageBox.Show("Credits", text, false);
        }

    }
}
