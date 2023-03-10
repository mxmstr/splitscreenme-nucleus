using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Forms
{
    public partial class CustomPrompt : Form
    {
        private int index;

        public CustomPrompt(string message, string prevAnswer, int i)
        {
            string theme = Globals.Theme;
            BackgroundImage = Image.FromFile(theme + "other_backgrounds.jpg");
            InitializeComponent();
            lbl_Desc.Text = message;
            txt_UserInput.Text = prevAnswer;

            index = i;
            TopMost = true;
            TopMost = false;
            TopMost = true;

            WindowScrape.Static.HwndInterface.MakeTopMost(Handle);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            CustomPromptRuntime.customValue[index] = txt_UserInput.Text;
            Close();
        }
    }
}
