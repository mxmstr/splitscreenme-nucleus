using Nucleus.Gaming.Cache;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Nucleus.Gaming.Forms
{
    public partial class CustomPrompt : Form
    {
        private int index;

        public CustomPrompt(string message, string prevAnswer, int i)
        {
            string theme = Globals.ThemeFolder;
            BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");
            InitializeComponent();
            lbl_Desc.Text = message;
            SetDescLabelLinkArea(message);


            lbl_Desc.LinkColor = Color.Orange;
            lbl_Desc.ActiveLinkColor = Color.DimGray;
            lbl_Desc.LinkClicked += new LinkLabelLinkClickedEventHandler(DescLabelLinkClicked);

            txt_UserInput.Text = prevAnswer;

            index = i;
            TopMost = true;
            TopMost = false;
            TopMost = true;

            WindowScrape.Static.HwndInterface.MakeTopMost(Handle);
        }

        private void DescLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = sender as LinkLabel;
            Process.Start(link.Tag.ToString());
        }

        private void SetDescLabelLinkArea(string value)
        {
            var wordList = value.Split(' ').ToList();
            var search = wordList.Where(word => word.StartsWith("http:") || word.StartsWith("file:") ||
                                                                      word.StartsWith("mailto:") || word.StartsWith("ftp:") ||
                                                                      word.StartsWith("https:") || word.StartsWith("gopher:") ||
                                                                      word.StartsWith("nntp:") || word.StartsWith("prospero:") ||
                                                                      word.StartsWith("telnet:") || word.StartsWith("news:") ||
                                                                      word.StartsWith("wais:") || word.StartsWith("outlook:")).FirstOrDefault();
            if (search != null)
            {
                lbl_Desc.LinkArea = new LinkArea(value.IndexOf(search), search.Length);
                lbl_Desc.Tag = search;
            }
            else
            {
                lbl_Desc.LinkArea = new LinkArea(0, 0);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            CustomPromptRuntime.customValue[index] = txt_UserInput.Text;
            Close();
        }
    }
}
