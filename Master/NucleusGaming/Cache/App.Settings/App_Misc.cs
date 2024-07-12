using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_Misc
    {
        private static string useNicksInGame;
        public static string UseNicksInGame
        {
            get => useNicksInGame;
            set { useNicksInGame = value; Globals.ini.IniWriteValue("Misc", "UseNicksInGame", value); }
        }

        private static string debugLog;
        public static string DebugLog
        {
            get => debugLog;
            set { debugLog = value; Globals.ini.IniWriteValue("Misc", "DebugLog", value); }
        }

        private static string enableLogAnswer;
        public static string EnableLogAnswer
        {
            get => enableLogAnswer;
            set { enableLogAnswer = value; Globals.ini.IniWriteValue("Misc", "EnableLogAnswer", value); }
        }

        private static string steamLang;
        public static string SteamLang
        {
            get => steamLang;
            set { steamLang = value; Globals.ini.IniWriteValue("Misc", "SteamLang", value); }
        }

        private static string epicLang;
        public static string EpicLang
        {
            get => epicLang;
            set { epicLang = value; Globals.ini.IniWriteValue("Misc", "EpicLang", value); }
        }

        private static string network;
        public static string Network
        {
            get => network;
            set { network = value; Globals.ini.IniWriteValue("Misc", "Network", value); }
        }

        private static string keepAccounts;
        public static string KeepAccounts
        {
            get => keepAccounts;
            set { keepAccounts = value; Globals.ini.IniWriteValue("Misc", "KeepAccounts", value); }
        }

        private static string disableForcedNote;
        public static string DisableForcedNote
        {
            get => disableForcedNote;
            set { disableForcedNote = value; Globals.ini.IniWriteValue("Misc", "DisableForcedNote", value); }
        }

        private static string nucleusAccountPassword;
        public static string NucleusAccountPassword
        {
            get => nucleusAccountPassword;
            set { nucleusAccountPassword = value; Globals.ini.IniWriteValue("Misc", "NucleusAccountPassword", value); }
        }

        private static string ignoreInputLockReminder;
        public static string IgnoreInputLockReminder
        {
            get => ignoreInputLockReminder;
            set { ignoreInputLockReminder = value; Globals.ini.IniWriteValue("Misc", "IgnoreInputLockReminder", value); }
        }

        private static string autoDesktopScaling;
        public static string AutoDesktopScaling
        {
            get => autoDesktopScaling;
            set { autoDesktopScaling = value; Globals.ini.IniWriteValue("Misc", "AutoDesktopScaling", value); }
        }

        private static string nucleusMultiInstances;
        public static string NucleusMultiInstances
        {
            get => nucleusMultiInstances;
            set { nucleusMultiInstances = value; Globals.ini.IniWriteValue("Misc", "NucleusMultiInstances", value); }
        }

        private static string disableGameProfiles;
        public static string DisableGameProfiles
        {
            get => disableGameProfiles;
            set { disableGameProfiles = value; Globals.ini.IniWriteValue("Misc", "DisableGameProfiles", value); }
        }

        private static string windowSize;
        public static string WindowSize
        {
            get => windowSize;
            set { windowSize = value; Globals.ini.IniWriteValue("Misc", "WindowSize", value); }
        }

        private static string windowLocation;
        public static string WindowLocation
        {
            get => windowLocation;
            set { windowLocation = value; Globals.ini.IniWriteValue("Misc", "WindowLocation", value); }
        }

        private static string autoSearchLocation;
        public static string AutoSearchLocation
        {
            get => autoSearchLocation;
            set { autoSearchLocation = value; Globals.ini.IniWriteValue("Misc", "AutoSearchLocation", value); }
        }

        private static string settingsLocation;
        public static string SettingsLocation
        {
            get => settingsLocation;
            set { settingsLocation = value; Globals.ini.IniWriteValue("Misc", "SettingsLocation", value); }
        }

        private static string profileSettingsLocation;
        public static string ProfileSettingsLocation
        {
            get => profileSettingsLocation;
            set { profileSettingsLocation = value; Globals.ini.IniWriteValue("Misc", "ProfileSettingsLocation", value); }
        }

        private static string gamepadSettingsLocation;
        public static string GamepadSettingsLocation
        {
            get => gamepadSettingsLocation;
            set { gamepadSettingsLocation = value; Globals.ini.IniWriteValue("Misc", "GamepadSettingsLocation", value); }
        }

        private static string theme;
        public static string Theme
        {
            get => theme;
            set { theme = value; Globals.ini.IniWriteValue("Theme", "Theme", value); }
        }

        private static string showFavoriteOnly;
        public static string ShowFavoriteOnly
        {
            get => showFavoriteOnly;
            set { showFavoriteOnly = value; Globals.ini.IniWriteValue("Dev", "ShowFavoriteOnly", value); }
        }

        private static string disablePathCheck;
        public static string DisablePathCheck
        {
            get => disablePathCheck;
            set { disablePathCheck = value; Globals.ini.IniWriteValue("Dev", "DisablePathCheck", value); }
        }

        private static string textEditorPath;
        public static string TextEditorPath
        {
            get => textEditorPath;
            set { textEditorPath = value; Globals.ini.IniWriteValue("Dev", "TextEditorPath", value); }
        }

        private static string useXinputIndex;
        public static string UseXinputIndex
        {
            get => useXinputIndex;
            set { useXinputIndex = value; Globals.ini.IniWriteValue("Dev", "UseXinputIndex", value); }
        }

        private static string blur;
        public static string Blur
        {
            get => blur;
            set { blur = value; Globals.ini.IniWriteValue("Dev", "Blur", value); }
        }

        private static string osdColor;
        public static string OSDColor
        {
            get => osdColor;
            set { osdColor = value; Globals.ini.IniWriteValue("Dev", "OSDColor", value); }
        }

        public static bool LoadSettings()
        {
            try
            {
                useNicksInGame = Globals.ini.IniReadValue("Misc", "UseNicksInGame");
                debugLog = Globals.ini.IniReadValue("Misc", "DebugLog");
                enableLogAnswer = Globals.ini.IniReadValue("Misc", "EnableLogAnswer");
                steamLang = Globals.ini.IniReadValue("Misc", "SteamLang");
                epicLang = Globals.ini.IniReadValue("Misc", "EpicLang");
                network = Globals.ini.IniReadValue("Misc", "Network");
                keepAccounts = Globals.ini.IniReadValue("Misc", "KeepAccounts");
                disableForcedNote = Globals.ini.IniReadValue("Misc", "DisableForcedNote");
                nucleusAccountPassword = Globals.ini.IniReadValue("Misc", "NucleusAccountPassword");
                ignoreInputLockReminder = Globals.ini.IniReadValue("Misc", "IgnoreInputLockReminder");
                autoDesktopScaling = Globals.ini.IniReadValue("Misc", "AutoDesktopScaling");
                nucleusMultiInstances = Globals.ini.IniReadValue("Misc", "NucleusMultiInstances");
                disableGameProfiles = Globals.ini.IniReadValue("Misc", "DisableGameProfiles");
                windowSize = Globals.ini.IniReadValue("Misc", "WindowSize");
                windowLocation = Globals.ini.IniReadValue("Misc", "WindowLocation");
                autoSearchLocation = Globals.ini.IniReadValue("Misc", "AutoSearchLocation");
                settingsLocation = Globals.ini.IniReadValue("Misc", "SettingsLocation");
                profileSettingsLocation = Globals.ini.IniReadValue("Misc", "ProfileSettingsLocation");
                gamepadSettingsLocation = Globals.ini.IniReadValue("Misc", "GamepadSettingsLocation");

                theme = Globals.ini.IniReadValue("Theme", "Theme");

                showFavoriteOnly = Globals.ini.IniReadValue("Dev", "ShowFavoriteOnly");
                disablePathCheck = Globals.ini.IniReadValue("Dev", "DisablePathCheck");
                textEditorPath = Globals.ini.IniReadValue("Dev", "TextEditorPath");
                useXinputIndex = Globals.ini.IniReadValue("Dev", "UseXinputIndex");
                blur = Globals.ini.IniReadValue("Dev", "Blur");
                osdColor = Globals.ini.IniReadValue("Dev", "OSDColor");

                
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it, show a message box, etc.
                return false;
            }

            return true;
        }

    }
}