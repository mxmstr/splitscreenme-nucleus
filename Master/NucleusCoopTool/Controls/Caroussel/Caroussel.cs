using Newtonsoft.Json.Linq;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public partial class Caroussel : UserControl
    {
       // private const string api = "https://hub.splitscreen.me/api/v1/";
        //private JArray handlers;
        //private int cover_index = 0;
        //private Label authorLabel;
        //private Label downloadLabel;
        //private Label hotnessLabel;
        //private System.Windows.Forms.Timer rainbowTimer;
        //private List<Label> labels;
        //private bool inited = false;
        private MainForm mainForm;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public Caroussel(MainForm _mainForm)
        {
            InitializeComponent();
            
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //BackgroundImage = ImageCache.GetImage(mainForm.theme + "showcase-background.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            mainForm = _mainForm;

            Main_Showcase(_mainForm);
        }

        public void Main_Showcase(MainForm mainForm)
        {
           // showcase_Label.Location = new Point(Width / 2 - showcase_Label.Width / 2, showcase_Label.Location.Y);
            var addNew = new BufferedClientAreaPanel();
            addNew.Size = new Size(170, 227);

            addNew.BackgroundImageLayout = ImageLayout.Center;
            addNew.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "add_game.png");
            addNew.Click += new EventHandler(mainForm.InsertWebview);

            showcaseBanner1.Controls.Add(addNew);

            List<UserGameInfo> games = GameManager.Instance.User.Games;

            //foreach (KeyValuePair<string, GenericGameInfo> con in GameManager.Instance.Games)
            for (int i = 0; i < games.Count; i++)
            {
                var game = games[i];
                Bitmap bmp = null;
                string coverPath = Path.Combine(Application.StartupPath, $"gui\\covers\\{game.GameGuid}.jpeg");

                if (File.Exists(coverPath))
                {
                    bmp = new Bitmap(Image.FromFile(coverPath));
                    var box = new BufferedClientAreaPanel();
                    box.Size = new Size(170, 227);
                    box.BackgroundImageLayout = ImageLayout.Stretch;
                    box.BackgroundImage = bmp;
                    box.Tag = game;
                    box.Click += CoverClick;
                    showcaseBanner1.Controls.Add(box);
                }

                if (bmp == null)
                {
                    Icon icon = Shell32.GetIcon(game.ExePath, false);
                    bmp = icon.ToBitmap();
                    icon.Dispose();

                    var box = new BufferedClientAreaPanel();
                    box.Size = new Size(170, 227);
                    box.BackgroundImageLayout = ImageLayout.Zoom;
                    box.BackgroundImage = bmp;
                    box.Tag = game;
                    box.Click += CoverClick;
                    showcaseBanner1.Controls.Add(box);
                }

                if (bmp == null)
                {
                    bmp = ImageCache.GetImage(Globals.ThemeFolder + "no_cover.png");
                    var box = new BufferedClientAreaPanel();
                    box.Size = new Size(170, 227);
                    box.BackgroundImageLayout = ImageLayout.Stretch;
                    box.BackgroundImage = bmp;
                    box.Tag = game;
                    box.Click += CoverClick;
                    showcaseBanner1.Controls.Add(box);
                }


               
            }

            //inited = true;
            //foreach (Control parent in Controls)
            //{
            //    foreach (Control childCon in parent.Controls)
            //    {
            //        foreach (Control coverBox in childCon.Controls)
            //        {
            //           // coverBox.BackgroundImage = showcaseCovers.Images[cover_index];

            //           // Panel labelContainer = new Panel()
            //           // {
            //           //     BackgroundImageLayout = ImageLayout.Stretch,
            //           //     Dock = DockStyle.Bottom,
            //           //     BackColor = Color.FromArgb(190, 0, 0, 0),
            //           //     Cursor = Theme_Settings.Default_Cursor
            //           // };

            //           // Panel coverLayer = new Panel()
            //           // {
            //           //     Location = new Point(0, 0),
            //           //     Size = new Size(coverBox.Width, 227),
            //           //     BackgroundImageLayout = ImageLayout.Stretch,
            //           //     Dock = DockStyle.Fill,
            //           //     BackColor = Color.Transparent,
            //           //     BackgroundImage = ImageCache.GetImage(mainForm.theme + "showcase_cover_layer.png"),
            //           //     Cursor = Theme_Settings.Hand_Cursor
            //           // };

            //           //// coverLayer.Name = $@"https://hub.splitscreen.me/handler/{hubLink[cover_index]}";
            //           // coverLayer.Click += new EventHandler(this.CoverBoxClick);

            //           // downloadLabel = new Label()
            //           // {
            //           //     AutoSize = true,
            //           //     Font = new Font("Franklin Gothic", 8, FontStyle.Regular, GraphicsUnit.Point, 0),
            //           //     BackColor = Color.Transparent,
            //           //     BackgroundImageLayout = ImageLayout.Stretch,
            //           //     TextAlign = ContentAlignment.TopLeft,
            //           //     Dock = DockStyle.Bottom,
            //           //     //Text = $"Downloads: {downloadCount[cover_index]}"
            //           // };

            //           // labelContainer.Controls.Add(downloadLabel);

            //           // hotnessLabel = new Label()
            //           // {
            //           //     AutoSize = true,
            //           //     Font = new Font("Franklin Gothic", 8, FontStyle.Regular, GraphicsUnit.Point, 0),
            //           //     BackColor = Color.Transparent,
            //           //     BackgroundImageLayout = ImageLayout.Stretch,
            //           //     TextAlign = ContentAlignment.TopLeft,
            //           //     //Text = $"Hotness: {hotness[cover_index]}"
            //           // };

            //           // labelContainer.Controls.Add(hotnessLabel);

            //           // authorLabel = new Label()
            //           // {
            //           //     AutoSize = true,
            //           //     Font = new Font("Franklin Gothic", 8, FontStyle.Bold, GraphicsUnit.Point, 0),
            //           //     BackColor = Color.Transparent,
            //           //     //Text = "Handler by " + author[cover_index],
            //           //     TextAlign = ContentAlignment.TopLeft,
            //           //     Dock = DockStyle.Bottom,
            //           // };

            //           // authorLabel.Location = new Point(downloadLabel.Location.X, downloadLabel.Top - authorLabel.Height);

            //           // labels.Add(authorLabel);
            //           // labelContainer.Controls.Add(authorLabel);

            //           // authorLabel.BringToFront();

            //           // labelContainer.Size = new Size(coverBox.Width, authorLabel.Height + downloadLabel.Height);
            //           // coverBox.Controls.Add(labelContainer);

            //           // hotnessLabel.Location = new Point(labelContainer.Right - hotnessLabel.Width, downloadLabel.Location.Y);
            //           // coverBox.Controls.Add(coverLayer);

            //           // //if (hotness[cover_index] == "0")
            //           //// {
            //           ////     hotness[cover_index] = ":(";
            //           //// }

            //           // cover_index++;
            //        }
            //    }
            //}

            // rainbowTimer.Start();
            //handlers.Clear();
            //Visible = true;
        }

        //private int r = 0;
        //private int g = 0;
        //private int b = 0;
        //private bool loop = false;

        //private void RainbowTimerTick(Object Object, EventArgs EventArgs)
        //{
        //    if (!loop)
        //    {
        //        if (r < 200 && g < 200 && b < 200) { r += 3; g += 3; b += 3; };
        //        if (r >= 200 && g >= 200 && b >= 200)
        //            loop = true;
        //    }
        //    else
        //    {
        //        if (r > 125 && g > 125 && b > 125) { r -= 3; g -= 3; b -= 3; }
        //        if (r <= 125 && g <= 125 && b <= 125)
        //            loop = false;
        //    }

        //    foreach (Control label in labels)
        //    {
        //        label.ForeColor = Color.FromArgb(r, g, 0);
        //    }
        //}

        //private void CoverBoxClick(object sender, EventArgs e)
        //{
        //    Panel _sender = (Panel)sender;
        //    string link = _sender.Name;
        //    Process.Start(link);
        //}


        private void CoverClick(object sender, EventArgs e)
        {
            var game = sender as BufferedClientAreaPanel;
            //mainForm.List_Games_SelectedChanged(game.Tag,null);
            //if (showcaseBanner1.VerticalScroll.SmallChange > 0)
            //{
            //    int scrolling = showcaseBanner1.HorizontalScroll.SmallChange;
            //    if (scrolling != 0  && inited)
            //    {
            //        showcaseBanner1.Update();
            //    }
            //}
        }
        private void ShowcaseBanner1_Paint(object sender, PaintEventArgs e)
        {
           

            //if (showcaseBanner1.VerticalScroll.SmallChange > 0)
            //{
            //    int scrolling = showcaseBanner1.HorizontalScroll.SmallChange;
            //    if (scrolling != 0  && inited)
            //    {
            //        showcaseBanner1.Update();
            //    }
            //}
        }

        private void Container1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Caroussel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle backBrushbounds = new Rectangle(0, 0, Width / 2, Height);

            Color color1 = Color.FromArgb(170, 15, 15, 15);
            Color color2 = Color.FromArgb(190, 15, 15, 15);
            Color color3 = Color.FromArgb(255, 15, 15, 15);
            Color color4 = Color.FromArgb(255, 15, 15, 15);

            LinearGradientBrush back_LinearGradientBrush =
                new LinearGradientBrush(backBrushbounds, Color.Transparent, color1, 90f);

            ColorBlend backcblend = new ColorBlend(6);
            backcblend.Colors = new Color[6] { Color.Transparent, color1, color2, color3, color3, color3 };
            backcblend.Positions = new float[6] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

            back_LinearGradientBrush.InterpolationColors = backcblend;

            e.Graphics.FillRectangle(back_LinearGradientBrush, ClientRectangle);
            back_LinearGradientBrush.Dispose();
        }
    }
}
