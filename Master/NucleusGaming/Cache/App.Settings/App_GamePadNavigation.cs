using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_GamePadNavigation
    {
        private static string type;
        public static string Type
        {
            get => type;
            set
            {
                type = value;
                Globals.ini.IniWriteValue("XUINav", "Type", value);
            }
        }

        private static string enabled;
        public static string Enabled
        {
            get => enabled;
            set
            {
                enabled = value; Globals.ini.IniWriteValue("XUINav", "Enabled", value);
            }
        }

        private static string deadzone;
        public static string Deadzone
        {
            get => deadzone;
            set
            {
                deadzone = value; Globals.ini.IniWriteValue("XUINav", "Deadzone", value);
            }
        }

        private static string dragDrop;
        public static string DragDrop
        {
            get => dragDrop;
            set
            {
                dragDrop = value;
                Globals.ini.IniWriteValue("XUINav", "DragDrop", value);
            }
        }

        private static string rightClick;
        public static string RightClick
        {
            get => rightClick;
            set
            {
                rightClick = value;
                Globals.ini.IniWriteValue("XUINav", "RightClick", value);
            }
        }

        private static string leftClick;
        public static string LeftClick
        {
            get => leftClick;
            set
            {
                leftClick = value; Globals.ini.IniWriteValue("XUINav", "LeftClick", value);
            }
        }

        private static Tuple<string, string> togglekUINavigation;
        public static Tuple<string, string> TogglekUINavigation
        {
            get => togglekUINavigation;
            set
            {
                togglekUINavigation = value;
                Globals.ini.IniWriteValue("XUINav", "LockUIControl", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> openOsk;
        public static Tuple<string, string> OpenOsk
        {
            get => openOsk;
            set
            {
                openOsk = value;
                Globals.ini.IniWriteValue("XUINav", "OpenOsk", $"{value.Item1}+{value.Item2}");
            }
        }

        public static bool LoadSettings()
        {
            type = Globals.ini.IniReadValue("XUINav", "Type");
            enabled = Globals.ini.IniReadValue("XUINav", "Enabled");
            deadzone = Globals.ini.IniReadValue("XUINav", "Deadzone");
            dragDrop = Globals.ini.IniReadValue("XUINav", "DragDrop");
            rightClick = Globals.ini.IniReadValue("XUINav", "RightClick");
            leftClick = Globals.ini.IniReadValue("XUINav", "LeftClick");
            togglekUINavigation = Tuple.Create(Globals.ini.IniReadValue("XUINav", "LockUIControl").Split('+')[0], Globals.ini.IniReadValue("XUINav", "LockUIControl").Split('+')[1]);
            openOsk = Tuple.Create(Globals.ini.IniReadValue("XUINav", "OpenOsk").Split('+')[0], Globals.ini.IniReadValue("XUINav", "OpenOsk").Split('+')[1]);

            return true;
        }
    }
}
