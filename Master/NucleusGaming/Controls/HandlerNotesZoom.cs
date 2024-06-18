using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public partial class HandlerNotesZoom : BufferedClientAreaPanel, IDynamicSized
    {
        public TransparentRichTextBox Notes => TextBox;
        private Pen linePen;
        private SolidBrush topBrush;

        private string customFont;

        public HandlerNotesZoom()
        {
            customFont = Globals.ThemeConfigFile.IniReadValue("Font", "FontFamily");

            InitializeComponent();

            ForeColor = Color.FromArgb(int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteFont").Split(',')[0]),
                                       int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteFont").Split(',')[1]), 
                                       int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteFont").Split(',')[2]));
            
            BackColor = Color.FromArgb(int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteZoomBackground").Split(',')[0]),
                                       int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteZoomBackground").Split(',')[1]),
                                       int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteZoomBackground").Split(',')[2]), 
                                       int.Parse(Globals.ThemeConfigFile.IniReadValue("Colors", "HandlerNoteZoomBackground").Split(',')[3])); 

            close_Btn.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close.png");
            close_Btn.BackColor = Color.Transparent;
            close_Btn.Cursor = new Cursor(Globals.ThemeFolder + "cursor_hand.ico");

            linePen = new Pen(TextBox.ForeColor, 1);

            MouseDown += HandlerNotesZoom_MouseDown;

            DPIManager.Register(this);
        }

        private void TextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.LinkText);
            }
            catch
            { }
        }

        private void Close_Btn_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void Close_Btn_MouseEnter(object sender, EventArgs e)
        {
            close_Btn.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close_mousehover.png");
        }

        private void Close_Btn_MouseLeave(object sender, EventArgs e)
        {
            close_Btn.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close.png");
        }

        public void UpdateSize(float scale)
        {
            close_Btn.Size = new Size((int)(20 * scale), (int)(20 * scale));
            close_Btn.Location = new Point(Width / 2 - (close_Btn.Width / 2), (Height - close_Btn.Height) - 10);
            warning.Height = (int)(warning.Height * scale);
            TextBox.Height -= warning.Height + close_Btn.Height;
            TextBox.Location = new Point(0, warning.Bottom + 10);
            TextBox.Font = new Font(customFont, 18f * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void HandlerNotesZoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, "Nucleus Co-op");
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        private void HandlerNotesZoom_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.DrawEllipse(linePen, close_Btn.Location.X - 6, close_Btn.Location.Y - 6, close_Btn.Width + 12, close_Btn.Height + 12);            
        }
    }
}
