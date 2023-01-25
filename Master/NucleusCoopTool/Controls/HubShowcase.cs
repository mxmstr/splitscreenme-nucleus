using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop.Generic;
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
        private MainForm mainForm;
        private JArray handlers;
        private int cover_index = 0;
        private Label authorLabel;
        private Label downloadLabel;
        private System.Windows.Forms.Timer rainbowTimer;
        private List<Label> labels;
        private Hub hub = new Hub();

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
            this.mainForm = mainForm;
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BackgroundImage = new Bitmap(mainForm.theme + "showcase-background.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            labels = new List<Label>();
            rainbowTimer = new System.Windows.Forms.Timer();
            rainbowTimer.Interval = (25); //millisecond                   
            rainbowTimer.Tick += new EventHandler(rainbowTimerTick);
            Main_Showcase();
        }

        public void Main_Showcase()
        {

            ScriptDownloader scriptDownloader = new ScriptDownloader(mainForm);
            ImageList showcaseCovers = new ImageList
            {
                ImageSize = new Size(170, 227)
            };

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.DefaultConnectionLimit = 9999;

            string rawHandlers = null;

            rawHandlers = hub.Get(api + "allhandlers");

            if (rawHandlers == null)
            {
                return;
            }
            else if (rawHandlers == "{}")
            {
                return;
            }

            JObject jObject = JsonConvert.DeserializeObject(rawHandlers) as JObject;

            JArray array = jObject["Handlers"] as JArray;
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

                Bitmap bmp = new Bitmap(Globals.Theme + "no_cover.png");

                string _cover = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{GameCover}.jpg";

                try
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_cover);
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
                        Panel coverLayer = new Panel()
                        {
                            Location = new Point(0, 0),
                            Size = new Size(coverBox.Width, 227),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Dock = DockStyle.Fill,
                            BackColor = Color.Transparent,
                            BackgroundImage = new Bitmap(mainForm.theme + "showcase_cover_layer.png"),
                            Cursor = mainForm.hand_Cursor
                        };

                        coverLayer.Click += new EventHandler(this.coverBoxClick);

                        authorLabel = new Label()
                        {
                            Font = new Font("Franklin Gothic", 8, FontStyle.Bold, GraphicsUnit.Point, 0),
                            BackColor = Color.Transparent,
                            BackgroundImage = new Bitmap(mainForm.theme + "showcase-labels.png"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            TextAlign = ContentAlignment.TopLeft,
                            Dock = DockStyle.Bottom
                        };

                        downloadLabel = new Label()
                        {
                            Font = new Font("Franklin Gothic", 8, FontStyle.Regular, GraphicsUnit.Point, 0),
                            BackColor = Color.Transparent,//Color.DimGray,
                            BackgroundImage = new Bitmap(mainForm.theme + "showcase-labels.png"),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            TextAlign = ContentAlignment.TopLeft,
                            Dock = DockStyle.Bottom
                        };

                        if (hotness[cover_index] == "0")
                        {
                            hotness[cover_index] = ":(";
                        }

                        coverLayer.Name = $@"https://hub.splitscreen.me/handler/{hubLink[cover_index]}";//lazy or stupid? Maybe both :).
                        authorLabel.Text = "Handler by " + author[cover_index];
                        downloadLabel.Text = "Downloads: " + downloadCount[cover_index] + "         " + " Hotness: " + hotness[cover_index];
                        coverBox.BackgroundImage = showcaseCovers.Images[cover_index];
                        labels.Add(authorLabel);
                        coverBox.Controls.Add(authorLabel);
                        coverBox.Controls.Add(downloadLabel);
                        coverBox.Controls.Add(coverLayer);
                        authorLabel.BringToFront();
                        cover_index++;
                    }
                }
            }

            rainbowTimer.Start();
            handlers.Clear();

        }

        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;

        private void rainbowTimerTick(Object Object, EventArgs EventArgs)
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

        private void coverBoxClick(object sender, EventArgs e)
        {
            Panel _sender = (Panel)sender;
            string link = _sender.Name;
            Process.Start(link);
        }

        private void showcaseBanner1_Paint(object sender, PaintEventArgs e)
        {
            if (showcaseBanner1.VerticalScroll.SmallChange > 0)
            {
                int scrolling = 0;
                scrolling = showcaseBanner1.HorizontalScroll.SmallChange;
                if (scrolling != 0)
                {
                    showcaseBanner1.Update();
                }
            }
        }

    }
}
