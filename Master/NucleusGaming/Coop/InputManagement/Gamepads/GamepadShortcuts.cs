using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Coop.InputManagement.Structs;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows;
using SharpDX.XInput;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Win32;

namespace Nucleus.Gaming.Coop.InputManagement.Gamepads
{
    public static class GamepadShortcuts
    {
        private static IniFile ini = Globals.ini;

        private static int SetFocus;
        private static int Close;
        private static int StopSession;
        private static int TopMost;
        private static int ResetWindows;
        private static int Cutscenes;
        private static int SwitchLayout;
        private static int LockInputs;
        private static int ReleaseCursor;
        public static int Pressed;
        public static int RightTriggerValue;
        public static int LeftTriggerValue;
        public static (int, int) RightStickValue;
        public static (int, int) LeftstickValue;

        private static int RT = 9999;
        private static int LT = 10000;

        private static bool ToggleCutscenes;

        public static Thread GamepadShortcutsThread;
        private static State previousState;

        /// <summary>
        /// Controller shortcuts handling thread.
        /// </summary>
        public static void GamepadShortcutsUpdate()
        {
            while (true)
            {
                for (int i = 0; i < GamepadState.Controllers.Length; i++)
                {
                   
                    if (!GamepadState.Controllers[i].IsConnected)
                    {
                        continue;
                    }

                    while (GenericGameHandler.Instance == null)
                    {
                        Thread.Sleep(1000);
                    }

                    while (GenericGameHandler.Instance.hasEnded)
                    {
                        Thread.Sleep(1000);
                    }

                    State currentState = (State)GamepadState.GetControllerState(i);

                    if (previousState.PacketNumber != currentState.PacketNumber)
                    {
                        int button = GamepadState.GetPressedButtons(i);
                        int rt = GamepadState.GetRightTriggerValue(i) > 0 ? button + RT : RT;///return RT + button or RT
                        int lt = GamepadState.GetLeftTriggerValue(i) > 0 ? button + LT : LT;///return LT + button or LT

                        if ((button == Cutscenes || rt == Cutscenes || lt == Cutscenes) && GameProfile.Saved)///cutscenes mode
                        {
                            if (!ToggleCutscenes)
                            {
                                GlobalWindowMethods.ToggleCutScenesMode(true);
                                ToggleCutscenes = true;
                            }
                            else
                            {
                                GlobalWindowMethods.ToggleCutScenesMode(false);
                                ToggleCutscenes = false;
                            }
                        }
                        else if ((button == SwitchLayout || rt == SwitchLayout || lt == SwitchLayout) && GameProfile.Saved)///Switch layout
                        {
                            GlobalWindowMethods.SwitchLayout();
                        }
                        else if ((button == ResetWindows || rt == ResetWindows || lt == ResetWindows) && GameProfile.Saved)///Reset windows
                        {
                            if (GenericGameHandler.Instance != null)
                                GenericGameHandler.Instance.Update(GenericGameHandler.Instance.HWndInterval, true);
                            Globals.MainOSD.Show(1600, $"Reseting game windows. Please wait...");

                        }
                        else if ((button == SetFocus || rt == SetFocus || lt == SetFocus))///Unfocus windows
                        {
                            GlobalWindowMethods.ChangeForegroundWindow();
                            Globals.MainOSD.Show(1600, $"Game Windows Unfocused");
                        }
                        else if ((button == TopMost || rt == TopMost || lt == TopMost))///Minimize/restore windows
                        {
                            GlobalWindowMethods.ShowHideWindows(GameProfile.Game);
                        }
                        else if (button == StopSession || rt == StopSession || lt == StopSession)///End current session
                        {
                            if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                            {
                                if (GenericGameHandler.Instance != null)
                                    GenericGameHandler.Instance.End(true);
                                Globals.MainOSD.Show(1600, $"Session Ended");
                            }
                            else
                            {
                                Globals.MainOSD.Show(1600, $"Unlock Inputs First");
                            }
                        }
                        else if (button == Close || rt == Close || lt == Close)///Close nucleus
                        {
                            if (GenericGameHandler.Instance != null)
                            {
                                GenericGameHandler.Instance.End(false);
                            }

                            User32Util.ShowTaskBar();
                            Globals.MainOSD.Show(1600, $"See You Later!");
                            Thread.Sleep(5000);

                            if (GenericGameHandler.Instance == null)
                            {
                                Process nc = Process.GetCurrentProcess();
                                nc.Kill();
                            }
                        }
                        else if ((button == LockInputs || rt == LockInputs || lt == LockInputs) && GameProfile.Saved)///Lock k&m inputs
                        {
                            if (!LockInput.IsLocked)
                            {
                                Globals.MainOSD.Show(1000, "Inputs Locked");

                                LockInput.Lock(GameProfile.Game?.LockInputSuspendsExplorer ?? true, GameProfile.Game?.ProtoInput.FreezeExternalInputWhenInputNotLocked ?? true, GameProfile.Game?.ProtoInput);

                                if (GameProfile.Game.ToggleUnfocusOnInputsLock)
                                {
                                    GlobalWindowMethods.ChangeForegroundWindow();
                                    Debug.WriteLine("Toggle Unfocus");
                                }
                            }
                            else
                            {
                                LockInput.Unlock(GameProfile.Game?.ProtoInput.FreezeExternalInputWhenInputNotLocked ?? true, GameProfile.Game?.ProtoInput);
                                Globals.MainOSD.Show(1000, "Inputs Unlocked");
                            }
                        }
                        else if (button == ReleaseCursor || rt == LockInputs || lt == LockInputs)///Try to release the cursor from game window by alt+tab inputs
                        {
                            //Todo Try by pausing focus loop instead so we can release the cursor from it's current window(but what for protoinput hooks?)
                            SendKeys.SendWait("%+{TAB}");
                            Thread.Sleep(500);
                        }

                        Thread.Sleep(135);
                        previousState = currentState;
                        Pressed = button;
                    }

                    RightTriggerValue = GamepadState.GetRightTriggerValue(i);
                    LeftTriggerValue = GamepadState.GetLeftTriggerValue(i);
                }
            }
        }

        public static void UpdateShortcutsValue()
        {
            if (ini.IniReadValue("XShortcuts", "SetFocus").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "SetFocus").Split('+');
                SetFocus = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "SetFocus", "");
            }

            if (ini.IniReadValue("XShortcuts", "Close").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Close").Split('+');
                Close = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Close", "");
            }

            if (ini.IniReadValue("XShortcuts", "Stop").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Stop").Split('+');
                StopSession = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Stop", "");
            }

            if (ini.IniReadValue("XShortcuts", "TopMost").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "TopMost").Split('+');
                TopMost = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "TopMost", "");
            }

            if (ini.IniReadValue("XShortcuts", "ResetWindows").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "ResetWindows").Split('+');
                ResetWindows = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ResetWindows", "");
            }

            if (ini.IniReadValue("XShortcuts", "Cutscenes").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Cutscenes").Split('+');
                Cutscenes = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Cutscenes", "");
            }

            if (ini.IniReadValue("XShortcuts", "Switch").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Switch").Split('+');
                SwitchLayout = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Switch", "");
            }

            if (ini.IniReadValue("XShortcuts", "LockInputs").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "LockInputs").Split('+');
                LockInputs = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XShortcuts", "ReleaseCursor").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+');
                ReleaseCursor = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ReleaseCursor", "");
            }
        }
    }
}
