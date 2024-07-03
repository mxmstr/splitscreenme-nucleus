using Nucleus.Coop.Forms;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows;
using SharpDX.XInput;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
        private static int Pressed;
        private static int RightTriggerValue;
        private static int LeftTriggerValue;

        private static int RT = 9999;
        private static int LT = 10000;

        public static Thread GamepadShortcutsThread;
        private static State previousState;

        /// <summary>
        /// Controller shortcuts handling thread.
        /// </summary>
        public static void GamepadShortcutsUpdate()
        {
            while (true)
            {
                while (GenericGameHandler.Instance == null)
                {
                    Thread.Sleep(5000);
                }

                while (GenericGameHandler.Instance.hasEnded)
                {
                    Thread.Sleep(5000);
                }

                for (int i = 0; i < GamepadState.Controllers.Length; i++)
                {
                    if (!GamepadState.Controllers[i].IsConnected)
                    {
                        continue;
                    }

                    State currentState = (State)GamepadState.GetControllerState(i);

                    if (previousState.PacketNumber != currentState.PacketNumber)
                    {
                        int button = GamepadState.GetPressedButtons(i);
                        int rt = GamepadState.GetRightTriggerValue(i) > 0 ? button + RT : RT;///return RT + button or RT
                        int lt = GamepadState.GetLeftTriggerValue(i) > 0 ? button + LT : LT;///return LT + button or LT
                        
                        if ((button == Cutscenes || rt == Cutscenes || lt == Cutscenes) && GameProfile.Saved)///cutscenes mode
                        {
                            GlobalWindowMethods.ToggleCutScenesMode();
                            Thread.Sleep(500);
                        }
                        else if ((button == SwitchLayout || rt == SwitchLayout || lt == SwitchLayout) && GameProfile.Saved)///Switch layout
                        {
                            GlobalWindowMethods.SwitchLayout();
                            Thread.Sleep(500);
                        }
                        else if ((button == ResetWindows || rt == ResetWindows || lt == ResetWindows) && GameProfile.Saved)///Reset windows
                        {
                            if (GenericGameHandler.Instance != null)
                                GlobalWindowMethods.ResetingWindows = true;
                            Thread.Sleep(500);

                        }
                        else if ((button == SetFocus || rt == SetFocus || lt == SetFocus))///Unfocus windows
                        {
                            GlobalWindowMethods.ChangeForegroundWindow();
                            Globals.MainOSD.Show(1600, $"Game Windows Unfocused");
                            Thread.Sleep(500);
                        }
                        else if ((button == TopMost || rt == TopMost || lt == TopMost))///Minimize/restore windows
                        {
                            GlobalWindowMethods.ShowHideWindows();
                            Thread.Sleep(500);
                        }
                        else if (button == StopSession || rt == StopSession || lt == StopSession)///End current session
                        {
                            if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                            {
                                if (GenericGameHandler.Instance != null)
                                {
                                    GenericGameHandler.Instance.End(true);
                                    Globals.MainOSD.Show(1600, $"Session Ended");
                                }
                            }
                            else
                            {
                                Globals.MainOSD.Show(1600, $"Unlock Inputs First");
                            }
                        }
                        else if (button == Close || rt == Close || lt == Close)///Close nucleus
                        {
                            GenericGameHandler.Instance?.End(false);

                            //User32Util.ShowTaskBar();

                            Thread.Sleep(5000);

                            if (GenericGameHandler.Instance.hasEnded)
                            {                               
                                Process.GetCurrentProcess().Kill();
                            }
                        }
                        else if ((button == LockInputs || rt == LockInputs || lt == LockInputs) && GameProfile.Saved)///Lock k&m inputs
                        {
                            if (!LockInput.IsLocked)
                            {
                                Globals.MainOSD.Show(1000, "Inputs Locked");

                                LockInput.Lock(GenericGameHandler.Instance.CurrentGameInfo?.LockInputSuspendsExplorer ?? true, GenericGameHandler.Instance.CurrentGameInfo?.ProtoInput.FreezeExternalInputWhenInputNotLocked ?? true, GameProfile.Game?.ProtoInput);

                                if (GenericGameHandler.Instance.CurrentGameInfo.ToggleUnfocusOnInputsLock)
                                {
                                    GlobalWindowMethods.ChangeForegroundWindow();
                                }

                                Thread.Sleep(1000);
                            }
                            else
                            {
                                LockInput.Unlock(GenericGameHandler.Instance.CurrentGameInfo?.ProtoInput.FreezeExternalInputWhenInputNotLocked ?? true, GenericGameHandler.Instance.CurrentGameInfo?.ProtoInput);
                                Globals.MainOSD.Show(1000, "Inputs Unlocked");
                            }
                        }
                        else if (button == ReleaseCursor || rt == LockInputs || lt == LockInputs)///Try to release the cursor from game window by alt+tab inputs
                        {
                            //Todo Try by pausing focus loop instead so we can release the cursor from it's current window(but what for protoinput hooks?)
                            SendKeys.SendWait("%+{TAB}");
                            Thread.Sleep(500);
                        }
                       
                        previousState = currentState;
                        Pressed = button;
                    }

                    RightTriggerValue = GamepadState.GetRightTriggerValue(i);
                    LeftTriggerValue = GamepadState.GetLeftTriggerValue(i);

                    #region Toggle shortcuts reminder window

                    if (Pressed == 1024)//Guide button
                    {
                        Thread.Sleep(500);//good enough to check for long press here

                        if (GamepadState.GetPressedButtons(i) == 1024)//good enough to check for long press here
                        {
                            foreach (ShortcutsReminder reminder in GenericGameHandler.Instance.shortcutsReminders)
                            {
                                reminder.Toggle(7);
                            }

                            Thread.Sleep(500);
                        }
                    }

                    #endregion                   
                }

                if (Pressed > 0)
                {
                    Thread.Sleep(350);
                }
                else
                {
                    Thread.Sleep(135);
                }
            }
        }

        public static void UpdateShortcutsValue()
        {
            if (ini.IniReadValue("XShortcuts", "SetFocus").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "SetFocus").Split('+');
                SetFocus = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "SetFocus", "");
            }

            if (ini.IniReadValue("XShortcuts", "Close").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Close").Split('+');
                Close = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Close", "");
            }

            if (ini.IniReadValue("XShortcuts", "Stop").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Stop").Split('+');
                StopSession = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Stop", "");
            }

            if (ini.IniReadValue("XShortcuts", "TopMost").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "TopMost").Split('+');
                TopMost = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "TopMost", "");
            }

            if (ini.IniReadValue("XShortcuts", "ResetWindows").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "ResetWindows").Split('+');
                ResetWindows = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ResetWindows", "");
            }

            if (ini.IniReadValue("XShortcuts", "Cutscenes").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Cutscenes").Split('+');
                Cutscenes = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Cutscenes", "");
            }

            if (ini.IniReadValue("XShortcuts", "Switch").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "Switch").Split('+');
                SwitchLayout = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Switch", "");
            }

            if (ini.IniReadValue("XShortcuts", "LockInputs").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "LockInputs").Split('+');
                LockInputs = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XShortcuts", "ReleaseCursor").Contains('+'))
            {
                string[] str = ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+');
                ReleaseCursor = int.Parse(str[0]) + int.Parse(str[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ReleaseCursor", "");
            }
        }
    }
}
