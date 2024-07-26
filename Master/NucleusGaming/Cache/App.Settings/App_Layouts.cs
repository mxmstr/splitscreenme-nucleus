using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_Layouts
    {
        private static string windowsMerger;
        public static string WindowsMerger
        {
            get => windowsMerger;
            set
            {
                windowsMerger = value; Globals.ini.IniWriteValue("CustomLayout", "WindowsMerger", value);
            }
        }

        private static string windowsMergerRes;
        public static string WindowsMergerRes
        {
            get => windowsMergerRes;
            set
            {
                windowsMergerRes = value;
                Globals.ini.IniWriteValue("CustomLayout", "WindowsMergerRes", value);
            }
        }

        private static string losslessHook;
        public static string LosslessHook
        {
            get => losslessHook;
            set
            {
                losslessHook = value;
                Globals.ini.IniWriteValue("CustomLayout", "LosslessHook", value);
            }
        }

        private static string splitDiv;
        public static string SplitDiv
        {
            get => splitDiv;
            set
            {
                splitDiv = value;
                Globals.ini.IniWriteValue("CustomLayout", "SplitDiv", value);
            }
        }

        private static string hideOnly;
        public static string HideOnly
        {
            get => hideOnly;
            set
            {
                hideOnly = value;
                Globals.ini.IniWriteValue("CustomLayout", "HideOnly", value);
            }
        }

        private static string splitDivColor;
        public static string SplitDivColor
        {
            get => splitDivColor;
            set
            {
                splitDivColor = value;
                Globals.ini.IniWriteValue("CustomLayout", "SplitDivColor", value);
            }
        }

        private static string horizontalLines;
        public static string HorizontalLines
        {
            get => horizontalLines;
            set
            {
                horizontalLines = value;
                Globals.ini.IniWriteValue("CustomLayout", "HorizontalLines", value);
            }
        }

        private static string verticalLines;
        public static string VerticalLines
        {
            get => verticalLines;
            set
            {
                verticalLines = value; Globals.ini.IniWriteValue("CustomLayout", "VerticalLines", value);
            }
        }

        private static string maxPlayers;
        public static string MaxPlayers
        {
            get => maxPlayers;
            set
            {
                maxPlayers = value;
                Globals.ini.IniWriteValue("CustomLayout", "MaxPlayers", value);
            }
        }

        private static string cts_KeepAspectRatio;
        public static string Cts_KeepAspectRatio
        {
            get => cts_KeepAspectRatio;
            set
            {
                cts_KeepAspectRatio = value;
                Globals.ini.IniWriteValue("CustomLayout", "Cts_KeepAspectRatio", value);
            }
        }

        private static string cts_MuteAudioOnly;
        public static string Cts_MuteAudioOnly
        {
            get => cts_MuteAudioOnly;
            set
            {
                cts_MuteAudioOnly = value;
                Globals.ini.IniWriteValue("CustomLayout", "Cts_MuteAudioOnly", value);
            }
        }

        private static string cts_Unfocus;
        public static string Cts_Unfocus
        {
            get => cts_Unfocus;
            set
            {
                cts_Unfocus = value;
                Globals.ini.IniWriteValue("CustomLayout", "Cts_Unfocus", value);
            }
        }

        public static bool LoadSettings()
        {
            windowsMerger = Globals.ini.IniReadValue("CustomLayout", "WindowsMerger");
            windowsMergerRes = Globals.ini.IniReadValue("CustomLayout", "WindowsMergerRes");
            losslessHook = Globals.ini.IniReadValue("CustomLayout", "LosslessHook");
            splitDiv = Globals.ini.IniReadValue("CustomLayout", "SplitDiv");
            hideOnly = Globals.ini.IniReadValue("CustomLayout", "HideOnly");
            splitDivColor = Globals.ini.IniReadValue("CustomLayout", "SplitDivColor");
            horizontalLines = Globals.ini.IniReadValue("CustomLayout", "HorizontalLines");
            verticalLines = Globals.ini.IniReadValue("CustomLayout", "VerticalLines");
            maxPlayers = Globals.ini.IniReadValue("CustomLayout", "MaxPlayers");
            cts_KeepAspectRatio = Globals.ini.IniReadValue("CustomLayout", "Cts_KeepAspectRatio");
            cts_MuteAudioOnly = Globals.ini.IniReadValue("CustomLayout", "Cts_MuteAudioOnly");
            cts_Unfocus = Globals.ini.IniReadValue("CustomLayout", "Cts_Unfocus");

            return true;
        }

    }
}
