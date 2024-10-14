using Nucleus.Gaming.Cache;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
//using System.IO;

namespace Nucleus.Gaming.Forms
{
    public partial class Prompt : Form
    {
        private bool hasOpenFileDialog = false;
        private string exeName;
        public bool onpaint = false;
        public Prompt(string message)
        {
            onpaint = false;
            InitializeComponent();
            BackgroundImage = Image.FromFile(Globals.ThemeFolder + "other_backgrounds.jpg");
            lbl_Msg.Text = message;

            lbl_Msg.LinkColor = Color.Orange;
            lbl_Msg.ActiveLinkColor = Color.DimGray;
            SetDescLabelLinkArea(message);
            lbl_Msg.LinkClicked += DescLabelLinkClicked;

            hasOpenFileDialog = false;

            TopMost = true;
            TopMost = false;
            TopMost = true;

            WindowScrape.Static.HwndInterface.MakeTopMost(Handle);
        }

        public Prompt(string message, bool onpaint)
        {
            InitializeComponent();
            BackgroundImage = Image.FromFile(Globals.ThemeFolder + "other_backgrounds.jpg");
            lbl_Msg.Text = message;
            SetDescLabelLinkArea(message);

            lbl_Msg.LinkColor = Color.Orange;
            lbl_Msg.ActiveLinkColor = Color.DimGray;
            lbl_Msg.LinkClicked += DescLabelLinkClicked;

            hasOpenFileDialog = false;

            TopMost = true;
            TopMost = false;
            TopMost = true;

            WindowScrape.Static.HwndInterface.MakeTopMost(Handle);
        }

        public Prompt(string message, bool isOFD, string launcherFileName)
        {
            onpaint = false;
            InitializeComponent();
            BackgroundImage = Image.FromFile(Globals.ThemeFolder + "other_backgrounds.jpg");
            lbl_Msg.Text = message;
            SetDescLabelLinkArea(message);

            lbl_Msg.LinkColor = Color.Orange;
            lbl_Msg.ActiveLinkColor = Color.DimGray;
            lbl_Msg.LinkClicked += DescLabelLinkClicked;

            hasOpenFileDialog = isOFD;
            exeName = launcherFileName;

            TopMost = true;
            TopMost = false;
            TopMost = true;

            WindowScrape.Static.HwndInterface.MakeTopMost(Handle);

            if (hasOpenFileDialog)
            {
                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.ShowHelp = true; //needed as a work-around to get the prompt to appear
                    if (!string.IsNullOrEmpty(exeName))
                    {
                        open.Title = $"Select the executable, {exeName}, as the launcher";
                        open.Filter = exeName + "|" + exeName;
                    }
                    else
                    {
                        open.Title = "Select the executable to be the launcher";
                        open.Filter = "Game Launcher Executable Files|*.exe";
                    }

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        Thread.Sleep(1000);
                        GenericGameHandler.ofdPath = open.FileName.Replace(@"\", @"\\");
                    }
                }
            }

            btn_Ok.PerformClick();
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
                lbl_Msg.LinkArea = new LinkArea(value.IndexOf(search), search.Length);
                lbl_Msg.Tag = search;
            }
            else
            {
                lbl_Msg.LinkArea = new LinkArea(0, 0);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Close();
        }
    }
}
