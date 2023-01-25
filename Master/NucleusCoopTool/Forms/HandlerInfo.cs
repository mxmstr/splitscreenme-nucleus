using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class HandlerInfo : BaseForm, IDynamicSized
    {
        protected string api = "https://hub.splitscreen.me/api/v1/";

        private readonly Handler Handler;
        private MainForm mainForm;
        private List<Control> ctrls = new List<Control>();
        private float fontSize;
        private Cursor default_Cursor;
        private Cursor hand_Cursor;
        public void button_Click(object sender, EventArgs e)
        {
            if (mainForm.mouseClick)
                mainForm.SoundPlayer(mainForm.theme + "button_click.wav");
        }

        private void controlscollect()
        {
            foreach (Control control in Controls)
            {
                ctrls.Add(control);
                foreach (Control container1 in control.Controls)
                {
                    ctrls.Add(container1);
                    foreach (Control container2 in container1.Controls)
                    {
                        ctrls.Add(container2);
                        foreach (Control container3 in container2.Controls)
                        {
                            ctrls.Add(container3);
                        }
                    }
                }
            }
        }

        public HandlerInfo(Handler handler, MainForm mf)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "HandlerInfoFontSize"));
            Color label_backColor = Color.FromArgb(0, 0, 0);
            Color label_foreColor = Color.FromArgb(255, 255, 255);

            InitializeComponent();
            SuspendLayout();
            default_Cursor = mf.default_Cursor;
            hand_Cursor = mf.hand_Cursor;

            Cursor = default_Cursor;

            ForeColor = label_foreColor;

            btn_Download.BackColor = mf.buttonsBackColor;
            btn_Close.BackgroundImage = new Bitmap(mf.theme + "title_close.png");
            btn_Close.FlatAppearance.MouseOverBackColor = Color.Transparent;

            BackColor = label_backColor;
            txt_Updated.BackColor = label_backColor;
            label10.BackColor = label_backColor;
            txt_Created.BackColor = label_backColor;
            label9.BackColor = label_backColor;
            txt_GameDesc.BackColor = label_backColor;
            txt_AuthDesc.BackColor = label_backColor;
            txt_Comm.BackColor = label_backColor;
            txt_Verified.BackColor = label_backColor;
            txt_Likes.BackColor = label_backColor;
            txt_Down.BackColor = label_backColor;
            txt_Version.BackColor = label_backColor;
            txt_GameName.BackColor = label_backColor;
            label8.BackColor = label_backColor;
            label7.BackColor = label_backColor;
            label6.BackColor = label_backColor;
            label5.BackColor = label_backColor;
            label3.BackColor = label_backColor;
            label2.BackColor = label_backColor;
            label1.BackColor = label_backColor;
            linkLabel_MoreInfo.BackColor = label_backColor;

            txt_Updated.ForeColor = label_foreColor;
            label10.ForeColor = label_foreColor;
            label9.ForeColor = label_foreColor;
            label8.ForeColor = label_foreColor;
            label7.ForeColor = label_foreColor;
            label6.ForeColor = label_foreColor;
            label5.ForeColor = label_foreColor;
            label3.ForeColor = label_foreColor;
            label2.ForeColor = label_foreColor;
            label1.ForeColor = label_foreColor;

            controlscollect();

            foreach (Control control in ctrls)
            {
                control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                if (control.GetType() == typeof(Button) || control.Name == "linkLabel_MoreInfo")
                {
                    control.Cursor = hand_Cursor;
                }
            }

            ResumeLayout();

            if (mf.mouseClick)
            {
                foreach (Control button in this.Controls) { if (button is Button && button.Name != "btn_Close") { button.Click += new System.EventHandler(this.button_Click); } }
            }

            this.mainForm = mf;

            Handler = handler;

            txt_GameName.Text = Handler.GameName;
            txt_GameDesc.Text = Handler.GameDescription;
            txt_Version.Text = Handler.CurrentVersion;
            txt_Down.Text = Handler.DownloadCount;
            txt_Likes.Text = Handler.Stars;

            if (Handler.Verified == "True")
            {
                txt_Verified.Text = "Yes";
            }
            else
            {
                txt_Verified.Text = "No";
            }

            txt_Created.Text = Handler.CreatedAt;
            if (int.Parse(Handler.CurrentVersion) > 1)
            {
                txt_Updated.Text = Handler.UpdatedAt;
            }
            else
            {
                txt_Updated.Text = "N/A";
            }

            txt_AuthDesc.Text = Handler.Description;

            Bitmap bmp = new Bitmap(mf.theme + "no_cover.png");
            string _cover = $@"https://images.igdb.com/igdb/image/upload/t_cover_small/{Handler.GameCover}.jpg";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
            ServicePointManager.DefaultConnectionLimit = 9999;

            try
            {
                WebRequest request = WebRequest.Create(_cover);
                //request.Timeout = 10;
                WebResponse resp = request.GetResponse();
                Stream respStream = resp.GetResponseStream();
                bmp = new Bitmap(respStream);
                respStream.Dispose();
            }
            catch (Exception) { }

            pic_GameCover.Image = bmp;


            string rawComments = Get(api + "comments/" + Handler.Id);
            if (rawComments != "{}")
            {
                JObject jObject = JsonConvert.DeserializeObject(rawComments) as JObject;

                JArray comments = jObject["Comments"] as JArray;
                for (int i = 0; i < comments.Count; i++)
                {
                    string id = jObject["Comments"][i]["_id"].ToString();
                    string owner = jObject["Comments"][i]["owner"].ToString();
                    string ownerName = jObject["Comments"][i]["ownerName"].ToString();
                    string content = jObject["Comments"][i]["content"].ToString();
                    string handlerId = jObject["Comments"][i]["handlerId"].ToString();
                    string createdAt = jObject["Comments"][i]["createdAt"].ToString();

                    txt_Comm.SelectionFont = new Font(txt_Comm.Font, FontStyle.Bold);
                    txt_Comm.AppendText(string.Format("{0} {1}", createdAt, ownerName));
                    txt_Comm.SelectionFont = new Font(txt_Comm.Font, FontStyle.Regular);
                    txt_Comm.AppendText(string.Format(": {0}", content));
                    txt_Comm.AppendText(Environment.NewLine);
                    txt_Comm.AppendText(Environment.NewLine);
                }
            }
            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
            SuspendLayout();

            if (scale > 1.0F)
            {
                float newFontSize = Font.Size * scale;
                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(RichTextBox) ^ c.GetType() == typeof(PictureBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
                    }
                }
            }

            ResumeLayout();
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            //request.Timeout = 10;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            DownloadPrompt downloadPrompt = new DownloadPrompt(Handler, mainForm, null);
            downloadPrompt.ShowDialog();
        }

        private void Label_MoreInfo_LinkClicked(object sender, EventArgs e)
        {
            Process.Start(Handler.GameUrl);
        }

        private void txt_AuthDesc_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void txt_Comm_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btn_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_Close.BackgroundImage = new Bitmap(mainForm.theme + "title_close_mousehover.png");
        }

        private void btn_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Close.BackgroundImage = new Bitmap(mainForm.theme + "title_close.png");
        }
    }
}
