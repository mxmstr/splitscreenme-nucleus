using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class OpenGameContentFolder
    {
        public static void OpenDataFolder(UserGameInfo currentGameInfo)
        {
            GameManager gameManager = GameManager.Instance;
            string path = Path.Combine(gameManager.GetAppContentPath(), currentGameInfo.Game.GUID);

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("No data present for this game.", "No data found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
