using Nucleus.Coop.Forms;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class ExtractHandler
    {
        public static void Extract(MainForm main)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select a game handler to extract",
                DefaultExt = "nc",
                InitialDirectory = Gaming.GameManager.Instance.GetJsScriptsPath(),
                Filter = "nc files (*.nc)|*.nc"
            };

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DownloadPrompt downloadPrompt = new DownloadPrompt(null, main, ofd.FileName);
                downloadPrompt.ShowDialog();
            }
        }
    }
}
