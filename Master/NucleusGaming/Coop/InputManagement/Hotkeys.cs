using Nucleus.Gaming.Windows.Interop;
using System.Windows.Forms;
using System;

namespace Nucleus.Gaming.Coop.InputManagement
{
    public static class Hotkeys
    {
        private static GenericGameInfo currentGameInfo;
        private static IntPtr formHandle;

        public const int KillProcess_HotkeyID = 1;
        public const int TopMost_HotkeyID = 2;
        public const int StopSession_HotkeyID = 3;
        public const int SetFocus_HotkeyID = 4;
        public const int ResetWindows_HotkeyID = 5;
        public const int Cutscenes_HotkeyID = 6;
        public const int Switch_HotkeyID = 7;
        public const int Reminder_HotkeyID = 8;
        public const int MergerFocusSwitch_HotkeyID = 9;

        public static int Custom_Hotkey_1 = 10;
        public static int Custom_Hotkey_2 = 11;
        public static int Custom_Hotkey_3 = 12;

        private static int GetMod(string modifier)
        {
            int mod = 0;
            switch (modifier)
            {
                case "Ctrl":
                    mod = 2;
                    break;
                case "Alt":
                    mod = 1;
                    break;
                case "Shift":
                    mod = 4;
                    break;
            }
            return mod;
        }

        public static void RegHotkeys(IntPtr _formHandle)
        {
            formHandle = _formHandle;
            try
            {
                User32Interop.RegisterHotKey(_formHandle, KillProcess_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, TopMost_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, StopSession_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, SetFocus_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, ResetWindows_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, Cutscenes_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, Switch_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, Reminder_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "ShortcutsReminder").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "ShortcutsReminder").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(_formHandle, MergerFocusSwitch_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "SwitchMergerChildForeGround").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "SwitchMergerChildForeGround").Split('+')[1].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering hotkeys " + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      

        public static void UnRegHotkeys()
        {
            try
            {
                User32Interop.UnregisterHotKey(formHandle, KillProcess_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, TopMost_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, StopSession_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, SetFocus_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, ResetWindows_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, Cutscenes_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, Switch_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, Reminder_HotkeyID);
                User32Interop.UnregisterHotKey(formHandle, MergerFocusSwitch_HotkeyID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error unregistering hotkeys " + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RegCustomHotkeys(GenericGameInfo _currentGameInfo)
        {
            currentGameInfo = _currentGameInfo;

            try
            {
                if (_currentGameInfo.CustomHotkeys != null)
                {                 
                    for (int i = 0; i < _currentGameInfo.CustomHotkeys.Length; i++)
                    {
                        string[] keys = _currentGameInfo.CustomHotkeys[i].Split('|');

                        switch (i)
                        {
                            case 0:
                                User32Interop.RegisterHotKey(formHandle, Custom_Hotkey_1, GetMod(keys[0]), (int)Enum.Parse(typeof(Keys), keys[1]));
                                break;
                            case 1:
                                User32Interop.RegisterHotKey(formHandle, Custom_Hotkey_2, GetMod(keys[0]), (int)Enum.Parse(typeof(Keys), keys[1]));
                                break;
                            case 2:
                                User32Interop.RegisterHotKey(formHandle, Custom_Hotkey_3, GetMod(keys[0]), (int)Enum.Parse(typeof(Keys), keys[1]));
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering hotkeys " + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UnRegCustomHotkeys()
        {
            if (currentGameInfo == null)
            {
                return;
            }

            try
            {
                if (currentGameInfo.CustomHotkeys != null)
                {
                    for (int i = 0; i < currentGameInfo.CustomHotkeys.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                User32Interop.UnregisterHotKey(formHandle, Custom_Hotkey_1);
                                break;
                            case 1:
                                User32Interop.UnregisterHotKey(formHandle, Custom_Hotkey_2);
                                break;
                            case 2:
                                User32Interop.UnregisterHotKey(formHandle, Custom_Hotkey_3);
                                break;
                        }                                          
                    }
                }

                currentGameInfo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering hotkeys " + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
