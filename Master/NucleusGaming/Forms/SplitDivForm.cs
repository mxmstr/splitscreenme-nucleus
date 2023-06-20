using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
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
        private System.Threading.Timer slideshow;
        private Timer loadTimer;
        private string currentGame;
        private System.Threading.Timer fading;
        private int alpha = 0;
        private bool stopPainting;

        public SplitForm(GenericGameInfo game, GenericGameHandler handler, Display screen)
        {
            InitializeComponent();
            Name = $"SplitForm{screen.DisplayIndex}";
            Text = $"SplitForm{screen.DisplayIndex}";
            Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            Width = screen.Bounds.Width;
            Height = screen.Bounds.Height;// + 50;
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Black;
            currentGame = game.GUID;
            Setup(game, handler);
        }

        private void Setup(GenericGameInfo game, GenericGameHandler handler)
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

            int interval = 5000;

            if (GameProfile.PauseBetweenInstanceLaunch > 0)
            {
                interval += (GameProfile.PauseBetweenInstanceLaunch * 1000) * handler.TotalPlayers;
            }
            else
            {
                interval += (game.PauseBetweenStarts * 1000) * handler.TotalPlayers;
            }

            loadTimer = new Timer
            {
                Interval = interval 
            };

            loadTimer.Tick += new EventHandler(loadTimerTick);
            loadTimer.Start();

            slideshow = new System.Threading.Timer(slideshowTick, null, 0, 0);
        }

        private bool fullApha;
        private void fadingTick(object state)
        {

            if (!fullApha)
            {
                alpha++;
            }

            if (alpha == 255)
            {
                if (Directory.Exists(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")))
                {
                    string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")));
                    Random rNum = new Random();
                    int RandomIndex = rNum.Next(0, imgsPath.Count());
                    
                    BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}\{RandomIndex}_{currentGame}.jpeg"));
                }

                fullApha = true;
            }

            if (fullApha)
            {
                alpha--;
            }

            if (alpha == 0)
            {
                fullApha = false;
            }

            Invalidate();
        }

        private void slideshowTick(object state)
        {
            if (fading == null)
            {
                fading = new System.Threading.Timer(fadingTick, null, 0, 30);

                if (Directory.Exists(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")))
                {
                    string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")));
                    Random rNum = new Random();
                    int RandomIndex = rNum.Next(0, imgsPath.Count());

                    BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}\{RandomIndex}_{currentGame}.jpeg"));
                }
            }
        }

        private void loadTimerTick(Object Object, EventArgs EventArgs)
        {
            slideshow.Dispose();
            loadTimer.Dispose();
            fading.Dispose();
            BackgroundImage = null;
            BackColor = ChoosenColor;
            stopPainting = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!stopPainting)
            {
                Rectangle back = new Rectangle(0, 0, Width, Height);
                SolidBrush backBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
                e.Graphics.FillRectangle(backBrush, back);
            }
        }
    }
}


