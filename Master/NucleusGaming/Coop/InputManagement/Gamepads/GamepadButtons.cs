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
            Bitmap bmp = null;
          
            switch (button)
            {
                case 1024://Guide

                return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\h.png");

                case 512://RightShoulder

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\rb.png");

                case 256://LeftShoulder

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\lb.png");

                case 128://RightThumb

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\rs.png");

                case 64://LeftThumb

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\ls.png");

                case 4096://A

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\b1.png");

                case 8192://B

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\b2.png");

                case 16384://X

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\b3.png");

                case 32768://Y

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\b4.png");

                case 32://Back

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\select.png");

                case 16://Start

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\start.png");

                case 1://DPadUp

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\du.png");

                case 2://DPadDown

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\dd.png");

                case 8://DPadRight

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\dr.png");

                case 4://DPadLeft

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\dl.png");

                case 9999://RightTrigger

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\rt.png");

                case 10000://LeftTrigger

                    return ImageCache.GetImage($"{Globals.Theme}gamepads\\{gamepadType}\\lt.png");
            }

            return bmp;
        }
    }
}
