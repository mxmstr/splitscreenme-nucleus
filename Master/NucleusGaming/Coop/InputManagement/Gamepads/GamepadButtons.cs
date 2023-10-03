using Games;
using Nucleus.Gaming.Cache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.InputManagement.Gamepads
{
    public static class GamepadButtons
    {
        private static readonly IniFile themeIni = Globals.ThemeIni;
        private static readonly string theme = Globals.Theme;



        public static Bitmap Image(int button,string gamepadType)
        {
            //gamepadType = \theme folder\gamepad buttons images folder\ 
            Bitmap bmp = null;

            switch (button)
            {
                case 1024:

                return ImageCache.GetImage(Globals.Theme /*+ gamepadType*/ + "no_cover.png");

                case 2:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 3:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 4:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 5:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 6:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 7:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 8:
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
                case 9 :
                    return ImageCache.GetImage(Globals.Theme + gamepadType + "no_cover.png");
            }

            return bmp;
        }





    }
}
