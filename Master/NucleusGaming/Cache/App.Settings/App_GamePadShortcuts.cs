using Nucleus.Gaming.Coop.InputManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_GamePadShortcuts
    {
        private static Tuple<string, string> close;
        public static Tuple<string, string> Close
        {
            get => close;
            set
            {
                close = value; Globals.ini.IniWriteValue("XShortcuts", "Close", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> stop;
        public static Tuple<string, string> StopSession
        {
            get => stop;
            set
            {
                stop = value; Globals.ini.IniWriteValue("XShortcuts", "Stop", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> topMost;
        public static Tuple<string, string> TopMost
        {
            get => topMost;
            set
            {
                topMost = value; Globals.ini.IniWriteValue("XShortcuts", "TopMost", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> setFocus;
        public static Tuple<string, string> SetFocus
        {
            get => setFocus;
            set
            {
                setFocus = value; Globals.ini.IniWriteValue("XShortcuts", "SetFocus", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> resetWindows;
        public static Tuple<string, string> ResetWindows
        {
            get => resetWindows;
            set
            {
                resetWindows = value;
                Globals.ini.IniWriteValue("XShortcuts", "ResetWindows", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> cutscenes;
        public static Tuple<string, string> CutscenesMode
        {
            get => cutscenes;
            set
            {
                cutscenes = value;
                Globals.ini.IniWriteValue("XShortcuts", "Cutscenes", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> _switch;
        public static Tuple<string, string> SwitchLayout
        {
            get => _switch;
            set
            {
                _switch = value; Globals.ini.IniWriteValue("XShortcuts", "Switch", $"{value.Item1}+{value.Item2}");
            }
        }

        //private static Tuple<string, string> shortcutsReminder;
        //public static Tuple<string, string> GPShorcut_ShortcutsReminder { get => shortcutsReminder; set { shortcutsReminder = value; Globals.ini.IniWriteValue("XShortcuts", "ShortcutsReminder", $"{value.Item1}+{value.Item2}"); } }

        //private static Tuple<string, string> switchMergerChildForeGround;
        //public static Tuple<string, string> GPShorcut_SwitchMergerForeGroundChild { get => switchMergerChildForeGround; set { switchMergerChildForeGround = value; Globals.ini.IniWriteValue("XShortcuts", "SwitchMergerChildForeGround", $"{value.Item1}+{value.Item2}"); } }

        private static Tuple<string, string> lockInputs;
        public static Tuple<string, string> LockInputs
        {
            get => lockInputs;
            set
            {
                lockInputs = value;
                Globals.ini.IniWriteValue("XShortcuts", "LockInputs", $"{value.Item1}+{value.Item2}");
            }
        }

        private static Tuple<string, string> releaseCursor;
        public static Tuple<string, string> ReleaseCursor
        {
            get => releaseCursor;
            set
            {
                releaseCursor = value;
                Globals.ini.IniWriteValue("XShortcuts", "ReleaseCursor", $"{value.Item1}+{value.Item2}");
            }
        }

        public static bool LoadSettings()
        {
            close = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "Close").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "Close").Split('+')[1]);
            stop = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "Stop").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "Stop").Split('+')[1]);
            topMost = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "TopMost").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "TopMost").Split('+')[1]);
            setFocus = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "SetFocus").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "SetFocus").Split('+')[1]);
            resetWindows = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "ResetWindows").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "ResetWindows").Split('+')[1]);
            cutscenes = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "Cutscenes").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "Cutscenes").Split('+')[1]);
            _switch = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "Switch").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "Switch").Split('+')[1]);
            //shortcutsReminder = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "ShortcutsReminder").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "ShortcutsReminder").Split('+')[1]);
            //switchMergerChildForeGround = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "SwitchMergerChildForeGround").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "SwitchMergerChildForeGround").Split('+')[1]);
            lockInputs = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "LockInputs").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "LockInputs").Split('+')[1]);
            releaseCursor = Tuple.Create(Globals.ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+')[0], Globals.ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+')[1]);

            return true;
        }
    }
}
