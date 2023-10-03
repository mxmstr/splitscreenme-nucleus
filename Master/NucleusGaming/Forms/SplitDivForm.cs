using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nucleus.Gaming.Forms
{
    public partial class SplitForm : Form
    {
        private Color ChoosenColor;
        private string gameGUID;
        private System.Threading.Timer fading;
        private int alpha = 0;
        private bool stopPainting;
        private SolidBrush backBrush;

        public SplitForm(GenericGameInfo game, Display screen)
        {
            InitializeComponent();
            Name = $"SplitForm{screen.DisplayIndex}";
            Text = $"SplitForm{screen.DisplayIndex}";
            Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            Width = screen.Bounds.Width;
            Height = screen.Bounds.Height;
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Black;
            gameGUID = game.GUID;
            game.OnFinishedSetup += SetupFinished;
            Setup();
        }

        private void Setup()
        {
            IDictionary<string, Color> splitColors = new Dictionary<string, Color>
            {
                { "Black", Color.Black },
                { "Gray", Color.DimGray },
                { "White", Color.White },
                { "Dark Blue", Color.DarkBlue },
                { "Blue", Color.Blue },
                { "Purple", Color.Purple },
                { "Pink", Color.Pink },
                { "Red", Color.Red },
                { "Orange", Color.Orange },
                { "Yellow", Color.Yellow },
                { "Green", Color.Green }
            };

            foreach (KeyValuePair<string, Color> color in splitColors)
            {
                if (color.Key != GameProfile.SplitDivColor)
                {
                    continue;
                }

                ChoosenColor = color.Value;
                break;
            }

            SlideshowStart();
        }

        private bool fullApha;
        private int imgIndex = 0;

        private void fadingTick(object state)
        {
            if (alpha == 255)
            {
                if (Directory.Exists(Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}")))
                {
                    string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}")));

                    BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"));

                    imgIndex++;

                    if (imgIndex == imgsPath.Length)
                    {
                        imgIndex = 0;
                    }
                }

                fullApha = true;
            }

            if (!fullApha)
            {
                alpha++;
            }

            if (fullApha)
            {
                alpha--;
            }

            if (alpha == 0)
            {
                fullApha = false;
            }

            backBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
            Invalidate();
        }

        private void SlideshowStart()
        {
            if (fading == null)
            {
                fading = new System.Threading.Timer(fadingTick, null, 0, 30);

                if (Directory.Exists(Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}")))
                {
                    string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}")));

                    BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"));
                    imgIndex++;
                }
            }
        }

        private void SetupFinished()
        {
            fading.Dispose();

            BackgroundImage = null;
            BackColor = ChoosenColor;
            Invalidate();
            stopPainting = true;    
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(backBrush, new Rectangle(0, 0, Width, Height));
          
            if (stopPainting)
            {
                return;
            }
        }
    }
}


