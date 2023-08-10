using Jint.Parser;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Nucleus.Gaming.Forms
{
    internal partial class CustomMessageBox : Form
    {
        private string message;
        private bool sized;

        internal CustomMessageBox(string title, string message, bool format)
        {
            string[] rgb_MouseOverColor = Globals.ThemeIni.IniReadValue("Colors", "MouseOver").Split(',');
            InitializeComponent();
            
            Text = title;
            this.message = format ? FormatText(message) : message;
            closeBtn.BackgroundImage = ImageCache.GetImage(Globals.Theme + "title_close.png");

            closeBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));
            BackgroundImage = ImageCache.GetImage(Globals.Theme + "other_backgrounds.jpg");
            Opacity = 0;//invisible until resized
            
        }

        private string FormatText(string message)
        {
            string removedNewLineJump = message.Replace('\n',' ');
            string[] words = removedNewLineJump.Split(' ');
            string formated = string.Empty;

            int wordsBeforeNewLine = 10;
            int start = wordsBeforeNewLine;

            for (int i = 0; i < words.Length; i++)
            {
                if (start == i)
                {
                    formated += words[i] + '\n';
                    start += wordsBeforeNewLine;
                }
                else
                {
                    if (words[i].EndsWith("."))
                    {
                        formated += words[i] + '\n';
                        start += wordsBeforeNewLine;
                    }
                    else 
                    {
                        formated += words[i] + ' ';
                    }                 
                }        
            }

            return formated;
        }

        private void CustomMessaBox_Paint(object sender, PaintEventArgs e)
        {     
            e.Graphics.DrawString(message, Font, Brushes.White, 10, 10);

            if (!sized)
            {
                SizeF newSize = e.Graphics.MeasureString(message, Font);
                Size = new Size((int)newSize.Width + 30, (int)newSize.Height + 30);
                MaximumSize = Size;
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width , Height , 20, 20));
                CenterToScreen();
                Opacity = 1.0F;
                sized = true;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void CustomMessageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }
    }
}
