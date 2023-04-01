using Nucleus.Gaming.Coop.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public static class Globals
    {
        public const string Version = "2.2.0";

        public static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));

        ///return theme path(current theme folder)
        public static string Theme => Path.Combine(Application.StartupPath, $@"gui\theme\{ini.IniReadValue("Theme", "Theme")}\");

        //return theme.ini file(current theme)
        public static IniFile ThemeIni => new IniFile(Path.Combine(Theme, "theme.ini"));

        public static OSD MainOSD = new OSD();
    }
}
