using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_Hotkeys
    {
        private static Tuple<string, string> close;
        public static Tuple<string, string> CloseApp
        {
            get => close;
            set
            {
                close = value;
                Globals.ini.IniWriteValue("Hotkeys", "Close", $"{value.Item1}+{value.Item2}");
            }
        }
    
        private static Tuple<string, string> stop;
        public static Tuple<string, string> StopSession 
        {
            get => stop; 
            set { 
                stop = value; 
                Globals.ini.IniWriteValue("Hotkeys", "Stop", $"{value.Item1}+{value.Item2}");
            } 
        }

        private static Tuple<string, string> topMost;
        public static Tuple<string, string> TopMost 
        { 
            get => topMost; 
            set {
                topMost = value; 
                Globals.ini.IniWriteValue("Hotkeys", "TopMost", $"{value.Item1}+{value.Item2}"); 
            }
        }

        private static Tuple<string, string> setFocus;
        public static Tuple<string, string> SetFocus 
        { 
            get => setFocus; 
            set 
            { 
                setFocus = value;
                Globals.ini.IniWriteValue("Hotkeys", "SetFocus", $"{value.Item1}+{value.Item2}"); 
            } 
        }

        private static Tuple<string, string> resetWindows;
        public static Tuple<string, string> ResetWindows 
        { 
            get => resetWindows; 
            set { resetWindows = value; 
                Globals.ini.IniWriteValue("Hotkeys", "ResetWindows", $"{value.Item1}+{value.Item2}"); 
            } 
        }

        private static Tuple<string, string> cutscenes;
        public static Tuple<string, string> CutscenesMode 
        { 
            get => cutscenes; 
            set 
            { 
                cutscenes = value; 
                Globals.ini.IniWriteValue("Hotkeys", "Cutscenes", $"{value.Item1}+{value.Item2}");
            } 
        }

        private static Tuple<string, string> _switch;
        public static Tuple<string, string> SwitchLayout 
        { 
            get => _switch; 
            set 
            { 
                _switch = value; 
                Globals.ini.IniWriteValue("Hotkeys", "Switch", $"{value.Item1}+{value.Item2}"); 
            } 
        }

        private static Tuple<string, string> shortcutsReminder;
        public static Tuple<string, string> ShortcutsReminder 
        { 
            get => shortcutsReminder; 
            set { shortcutsReminder = value; 
                Globals.ini.IniWriteValue("Hotkeys", "ShortcutsReminder", $"{value.Item1}+{value.Item2}"); 
            } 
        }

        private static Tuple<string, string> switchMergerChildForeGround;
        public static Tuple<string, string> SwitchMergerForeGroundChild
        { 
            get => switchMergerChildForeGround; 
            set { switchMergerChildForeGround = value; 
                Globals.ini.IniWriteValue("Hotkeys", "SwitchMergerChildForeGround", $"{value.Item1}+{value.Item2}");
            } 
        }

        private static string lockInputs;
        public static string LockInputs
        { 
            get => lockInputs; 
            set 
            { 
                lockInputs = value; 
                Globals.ini.IniWriteValue("Hotkeys", "LockKey", value); 
            } 
        }

        public static bool LoadSettings()
        {
            try
            {
                //Todo: Add check for empty fields and set default values
                close = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[1]);
                stop = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[1]);
                topMost = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1]);
                setFocus = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1]);
                resetWindows = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1]);
                cutscenes = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[1]);
                _switch = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[1]);
                shortcutsReminder = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "ShortcutsReminder").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "ShortcutsReminder").Split('+')[1]);
                switchMergerChildForeGround = Tuple.Create(Globals.ini.IniReadValue("Hotkeys", "SwitchMergerChildForeGround").Split('+')[0], Globals.ini.IniReadValue("Hotkeys", "SwitchMergerChildForeGround").Split('+')[1]);
                lockInputs = Globals.ini.IniReadValue("Hotkeys", "LockKey");
            }
            catch (Exception ex)
            {
                //message box
                return false;
            }

            return true;
        }
    }
}
