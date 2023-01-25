using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Forms
{
    public partial class SplitForm : Form
    {
        private Color ChoosenColor;
        private Timer slideshow;
        private Timer loadTimer;
        private string currentGame;

        public SplitForm(GenericGameInfo game, GenericGameHandler handler,Screen screen)
        {
            InitializeComponent();
            Name = "SplitForm";
            Text = "SplitForm";
            Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            Width = screen.WorkingArea.Size.Width;
            Height = screen.WorkingArea.Size.Height + 50;
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;           
            BackColor = Color.Black;
            currentGame = game.GUID;

            Setup(game, handler, screen);
        }


        private void Setup(GenericGameInfo game,GenericGameHandler handler, Screen screen)
        {
            IDictionary<string, Color> splitColors = new Dictionary<string, Color>();

            splitColors.Add("Black", Color.Black);
            splitColors.Add("Gray", Color.DimGray);
            splitColors.Add("White", Color.White);
            splitColors.Add("Dark Blue", Color.DarkBlue);
            splitColors.Add("Blue", Color.Blue);
            splitColors.Add("Purple", Color.Purple);
            splitColors.Add("Pink", Color.Pink);
            splitColors.Add("Red", Color.Red);
            splitColors.Add("Orange", Color.Orange);
            splitColors.Add("Yellow", Color.Yellow);
            splitColors.Add("Green", Color.Green);

            foreach (KeyValuePair<string, Color> color in splitColors)
            {
                if (color.Key == GameProfile.SplitDivColor)
                {
                    ChoosenColor = color.Value;
                }
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
            if (Directory.Exists(Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGame)))
            {
                string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGame)));
                Random rNum = new Random();
                int RandomIndex = rNum.Next(0, imgsPath.Count());

                BackgroundImage = new Bitmap(Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGame + "\\" + RandomIndex + "_" + currentGame + ".jpeg"));            
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


