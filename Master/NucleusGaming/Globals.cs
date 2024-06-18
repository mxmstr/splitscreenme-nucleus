using Nucleus.Gaming.Controls;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public static class Globals
    {
        public const string Version = "2.2.2";

        public static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));

        //return theme path(current theme folder)
        public static string ThemeFolder => Path.Combine(Application.StartupPath, $@"gui\theme\{ini.IniReadValue("Theme", "Theme")}\");

        //return theme.ini file(current theme)
        public static IniFile ThemeConfigFile => new IniFile(Path.Combine(ThemeFolder, "theme.ini"));

        public static Button PlayButton;
        public static HandlerNotesZoom HandlerNotesZoom;
        public static Button Btn_debuglog;
        public static Button ProfilesList_btn;

        public static readonly string GameProfilesFolder = Path.Combine(Application.StartupPath, $"game profiles");
        public static WPF_OSD MainOSD;
        public static readonly int NucleusMaxPlayers = 32;
    }
}
