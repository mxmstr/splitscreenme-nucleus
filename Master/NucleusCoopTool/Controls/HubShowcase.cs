using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public partial class HubShowcase : UserControl
    {
        private const string api = "https://hub.splitscreen.me/api/v1/";
        private JArray handlers;
        private int cover_index = 0;
        private Label authorLabel;
        private Label downloadLabel;
        private Label hotnessLabel;
        private System.Windows.Forms.Timer rainbowTimer;
        private List<Label> labels;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public HubShowcase(MainForm mainForm)
        {
            InitializeComponent();

            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BackgroundImage = ImageCache.GetImage(mainForm.theme + "showcase-background.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            labels = new List<Label>();

            rainbowTimer = new System.Windows.Forms.Timer
            {
                Interval = (25) //millisecond                   
            };

            rainbowTimer.Tick += new EventHandler(RainbowTimerTick);

            Main_Showcase(mainForm);
        }

        public void Main_Showcase(MainForm mainForm)
        {
            showcase_Label.Location = new Point(Width / 2 - showcase_Label.Width / 2, showcase_Label.Location.Y);
            ImageList showcaseCovers = new ImageList
            {
                ImageSize = new Size(170, 227)
            };

            JArray array = HubCache.InitCache();

            handlers = new JArray(array.OrderByDescending(obj => (DateTime)obj["createdAt"]));

            List<string> author = new List<string>();
            List<string> downloadCount = new List<string>();
            List<string> hubLink = new List<string>();
            List<string> hotness = new List<string>();

            for (int i = 0; i < 16; i++)
            {
                string GameCover = handlers[i]["gameCover"].ToString();
                author.Add(handlers[i]["ownerName"].ToString());
                downloadCount.Add(handlers[i]["downloadCount"].ToString());
                hubLink.Add(handlers[i]["_id"].ToString());
                hotness.Add(handlers[i]["stars"].ToString());

                Bitmap bmp = ImageCache.GetImage(Globals.ThemeFolder + "no_cover.png");

                string _cover = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{GameCover}.jpg";

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_cover);
                    request.UserAgent = "request";
                    WebResponse resp = request.GetResponse();
                    Stream respStream = resp.GetResponseStream();
                    bmp = new Bitmap(respStream);
                    respStream.Dispose();
                    showcaseCovers.Images.Add(bmp);
                }
                catch (Exception) { }
            }

            foreach (Control parent in Controls)
            {
                foreach (Control childCon in parent.Controls)
                {
                    foreach (Control coverBox in childCon.Controls)
                    {
                        coverBox.BackgroundImage = showcaseCovers.Images[cover_index];

                        Panel labelContainer = new Panel()
                        {
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Dock = DockStyle.Bottom,
                            BackColor = Color.FromArgb(190, 0, 0, 0),
                            Cursor = Theme_Settings.Default_Cursor
                        };

                        Panel coverLayer = new Panel()
                        {
                            Location = new Point(0, 0),
                            Size = new Size(coverBox.Width, 227),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Dock = DockStyle.Fill,
                            BackColor = Color.Transparent,
                            BackgroundImage = ImageCache.GetImage(mainForm.theme + "showcase_cover_layer.png"),
                            Cursor = Theme_Settings.Hand_Cursor
                        };

                        coverLayer.Name = $@"https://hub.splitscreen.me/handler/{hubLink[cover_index]}";
                        coverLayer.Click += new EventHandler(this.CoverBoxClick);

                        downloadLabel = new Label()
                        {
                            AutoSize = true,
                            Font = new Font("Franklin Gothic", 8, FontStyle.Regular, GraphicsUnit.Point, 0),
                            BackColor = Color.Transparent,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            TextAlign = ContentAlignment.TopLeft,
                            Dock = DockStyle.Bottom,
                            Text = $"Downloads: {downloadCount[cover_index]}"
                        };

                        labelContainer.Controls.Add(downloadLabel);

                        hotnessLabel = new Label()
                        {
                            AutoSize = true,
                            Font = new Font("Franklin Gothic", 8, FontStyle.Regular, GraphicsUnit.Point, 0),
                            BackColor = Color.Transparent,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            TextAlign = ContentAlignment.TopLeft,
                            Text = $"Hotness: {hotness[cover_index]}"
                        };

                        labelContainer.Controls.Add(hotnessLabel);

                        authorLabel = new Label()
                        {
                            AutoSize = true,
                            Font = new Font("Franklin Gothic", 8, FontStyle.Bold, GraphicsUnit.Point, 0),
                            BackColor = Color.Transparent,
                            Text = "Handler by " + author[cover_index],
                            TextAlign = ContentAlignment.TopLeft,
                            Dock = DockStyle.Bottom,
                        };

                        authorLabel.Location = new Point(downloadLabel.Location.X, downloadLabel.Top - authorLabel.Height);

                        labels.Add(authorLabel);
                        labelContainer.Controls.Add(authorLabel);

                        authorLabel.BringToFront();

                        labelContainer.Size = new Size(coverBox.Width, authorLabel.Height + downloadLabel.Height);
                        coverBox.Controls.Add(labelContainer);

                        hotnessLabel.Location = new Point(labelContainer.Right - hotnessLabel.Width, downloadLabel.Location.Y);
                        coverBox.Controls.Add(coverLayer);

                        if (hotness[cover_index] == "0")
                        {
                            hotness[cover_index] = ":(";
                        }

                        cover_index++;
                    }
                }
            }

            rainbowTimer.Start();
            handlers.Clear();
            Visible = true;
        }

        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;

        private void RainbowTimerTick(Object Object, EventArgs EventArgs)
        {
            if (!loop)
            {
                if (r < 200 && g < 200 && b < 200) { r += 3; g += 3; b += 3; };
                if (r >= 200 && g >= 200 && b >= 200)
                    loop = true;
            }
            else
            {
                if (r > 125 && g > 125 && b > 125) { r -= 3; g -= 3; b -= 3; }
                if (r <= 125 && g <= 125 && b <= 125)
                    loop = false;
            }

            foreach (Control label in labels)
            {
                label.ForeColor = Color.FromArgb(r, g, 0);
            }
        }

        private void CoverBoxClick(object sender, EventArgs e)
        {
            Panel _sender = (Panel)sender;
            string link = _sender.Name;
            Process.Start(link);
        }

        private void ShowcaseBanner1_Paint(object sender, PaintEventArgs e)
        {
            if (showcaseBanner1.VerticalScroll.SmallChange > 0)
            {
                int scrolling = showcaseBanner1.HorizontalScroll.SmallChange;
                if (scrolling != 0)
                {
                    showcaseBanner1.Update();
                }
            }
        }
    }
}
