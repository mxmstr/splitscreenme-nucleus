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
        private Timer slideshow;
        private Timer loadTimer;
        private string currentGame;

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

            Setup(game, handler, screen);
        }


        private void Setup(GenericGameInfo game, GenericGameHandler handler, Display screen)
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
                Interval = interval //millisecond
            };

            loadTimer.Tick += new EventHandler(loadTimerTick);
            loadTimer.Start();

            slideshow = new Timer
            {
                Interval = (8000) //millisecond
            };

            slideshow.Tick += new EventHandler(slideshowTick);
            slideshow.Start();

        }

        private void slideshowTick(Object myObject, EventArgs myEventArgs)
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")))
            {
                string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}")));
                Random rNum = new Random();
                int RandomIndex = rNum.Next(0, imgsPath.Count());

                BackgroundImage = ImageCache.GetImage(Path.Combine(Application.StartupPath, $@"gui\screenshots\{currentGame}\{RandomIndex}_{currentGame}.jpeg"));
            }
        }

        private void loadTimerTick(Object myObject, EventArgs myEventArgs)
        {
            slideshow.Dispose();
            loadTimer.Dispose();

            BackgroundImage = null;
            BackColor = ChoosenColor;
        }
    }
}


