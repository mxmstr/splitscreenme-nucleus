using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Coop.Tools
{
    public static class GetGameIcon
    {
        public static MainForm MainForm;
        public static void GetIcon(object state)
        {
            UserGameInfo game = (UserGameInfo)state;
            Bitmap bmp;

            string iconPath = game.Game.MetaInfo.IconPath;

            try
            {
                if (File.Exists(iconPath))
                {
                    if (iconPath.EndsWith(".exe"))
                    {
                        Icon icon = Shell32.GetIcon(iconPath, false);
                        bmp = icon.ToBitmap();
                        icon.Dispose();
                    }
                    else
                    {
                        bmp = ImageCache.GetImage(iconPath);
                    }
                }
                else
                {
                    Icon icon = Shell32.GetIcon(game.ExePath, false);
                    bmp = icon.ToBitmap();
                    icon.Dispose();
                }
            }
            catch
            {
                bmp = ImageCache.GetImage(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
            }

            if (bmp == null)
            {
                bmp = ImageCache.GetImage(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
            }

            game.Icon = bmp;

            lock (MainForm.controls)
            {
                if (MainForm.controls.ContainsKey(game))
                {
                    GameControl control = MainForm.controls[game];
                    control.Image = game.Icon;
                }
            }
        }
    }
}
