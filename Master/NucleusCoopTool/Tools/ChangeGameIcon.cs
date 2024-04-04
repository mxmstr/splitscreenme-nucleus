using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class ChangeGameIcon
    {
        public static void ChangeIcon(MainForm main, UserGameInfo userGameInfo, IniFile iconsIni)
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "All Images Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tiff;*.tif;*.ico;*.exe)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tiff;*.tif;*.ico;*.exe" +
                            "|PNG Portable Network Graphics (*.png)|*.png" +
                            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
                            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
                            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
                            "|Icon (*.ico)|*.ico" +
                            "|Executable (*.exe)|*.exe";
                dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.FileName.EndsWith(".exe"))
                    {
                        Icon icon = Shell32.GetIcon(dlg.FileName, false);

                        Bitmap bmp = icon.ToBitmap();
                        icon.Dispose();
                        userGameInfo.Icon = bmp;
                    }
                    else
                    {
                        userGameInfo.Icon = ImageCache.GetImage(dlg.FileName);
                    }

                    iconsIni.IniWriteValue("GameIcons", userGameInfo.Game.GameName, dlg.FileName);

                    main.GetIcon(userGameInfo);
                    main.RefreshGames();
                }
            }

        }
    }
}
