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

namespace Nucleus.Gaming.Coop.InputManagement
{
    public static class XController
    {
        [DllImport("xinput1_4.dll", EntryPoint = "#100")]//Enable Menu button on controllers
        public static extern int XInputGetStateSecret
        (
                 int dwUserIndex,// [in] Index of the device
                 ref State pState// [out] Receives the current state
        );

        public static Controller[] Controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };

        public static int GetPressedButtons(int index)///Return current pressed button of any available XInput Controller
        {
            if (Controllers[index].IsConnected)
            {
                State state = (State)GetControllerState(index);

                return (int)state.Gamepad.Buttons;
            }
            return 0;
        }

        public static int GetRightTriggerValue(int index)///Return current right trigger value of XInput Controller by index
        {
            if (Controllers[index].IsConnected)
            {
                State state = (State)GetControllerState(index);

                return (int)state.Gamepad.RightTrigger;
            }

            return 0;
        }

        public static int GetLeftTriggerValue(int index)///Return current left trigger value of XInput Controller by index
        {
            if (Controllers[index].IsConnected)
            {
                State state = (State)GetControllerState(index);

                return (int)state.Gamepad.LeftTrigger;
            }

            return 0;
        }

        public static (int, int) GetRightStickValue(int index)///Return current right stick value of XInput Controller by index
        {
            if (Controllers[index].IsConnected)
            {
                State state = (State)GetControllerState(index);

                return ((int)state.Gamepad.RightThumbX, (int)state.Gamepad.RightThumbY);
            }

            return (0, 0);
        }

        public static (int, int) GetLeftStickValue(int index)///Return current left stick value of XInput Controller by index
        {
            if (Controllers[index].IsConnected)
            {
                State state = (State)GetControllerState(index);

                return ((int)state.Gamepad.LeftThumbX, (int)state.Gamepad.LeftThumbY);
            }

            return (0, 0);
        }

        public static Object GetControllerState(int index)///Return current left stick value of XInput Controller by index
        {
            Controller controller = Controllers[index];

            if (controller.IsConnected)
            {
                var state = controller.GetState();

                XInputGetStateSecret(index, ref state);

                int.TryParse("None", out int noButtonPressed);

                return state;
            }

            return new State();
        }
    }

    public static class ControllersUINav
    {
        private static IniFile ini = Globals.ini;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        public static Thread controllersUINavThread;
        public static Thread ControllersUINavThread => controllersUINavThread;

        private static Point GetCursorPos()
        {
            GetCursorPos(out POINT cursor);
            return cursor;
        }

        private static int Deadzone;
        private static int Dragdrop;
        private static int RightClick;
        private static int LeftClick;
        private static int LockUIControl;
        private static int OpenOsk;
        private static int RT = 9999;
        private static int LT = 10000;

        private static bool _Enabled;
        public static bool Enabled => _Enabled;
        public static bool EnabledRuntime;///Can on/off UI navigation later on runtime
        private static bool oskCleared = false;
        private static bool oskVisible;


        public static void StartXConNavThread()
        {
            int x = GetCursorPos().X;///Init current cursor X
            int y = GetCursorPos().Y;///Init current cursor Y

            int steps = 1;///How much pixels the cursor will move
            int scrollStep = 1;
            ///Deadzone => Joystick value from where the cursor will start moving
            int pollRate = 10;///How often the thread will sleep
            int prevPressed = 0;///previous Xinput button state 
            bool dragging = false;
            EnabledRuntime = _Enabled;

            while (true)
            {
                for (int i = 0; i < XController.Controllers.Length; i++)
                {
                    if (!XController.Controllers[i].IsConnected)
                    {
                        continue;
                    }

                    GetCursorPos(out POINT cursor);
                    x = cursor.X;
                    y = cursor.Y;

                    int pressed = XController.GetPressedButtons(i);/// Current pressed Xinput button
                    int rt = XController.GetRightTriggerValue(i) > 0 ? pressed + RT : RT;//return RT + button or RT
                    int lt = XController.GetLeftTriggerValue(i) > 0 ? pressed + LT : LT;//return LT + button or LT

                    ///Adjust the cursor speed to the joystick value
                    int MouveLeftSpeed = (Math.Abs((XController.GetLeftStickValue(i).Item1) / 2000));
                    int MouveRightSpeed = ((XController.GetLeftStickValue(i).Item1) / 2000);
                    int MouveUpSpeed = (Math.Abs((XController.GetLeftStickValue(i).Item2) / 2000));
                    int MouveDownSpeed = ((XController.GetLeftStickValue(i).Item2) / 2000);

                    ///Check if the right joystick values are out of the deadzone and allow to move the cursor or not
                    bool canMouveLeft = XController.GetLeftStickValue(i).Item1 <= -Deadzone;
                    bool canMouveRight = XController.GetLeftStickValue(i).Item1 >= Deadzone;
                    bool canMouveUp = XController.GetLeftStickValue(i).Item2 <= -Deadzone;
                    bool canMouveDown = XController.GetLeftStickValue(i).Item2 >= Deadzone;

                    ///Adjust scrolling speed to the joystick value
                    int ScrollUpSpeed = Math.Abs((XController.GetRightStickValue(i).Item2) / 1000);
                    int ScrollDownSpeed = (XController.GetRightStickValue(i).Item2) / 1000;

                    ///Check if the left joystick value(Y axis only) is out of the deadzone and allow to scroll Up or Down or not
                    bool canScrollUp = XController.GetRightStickValue(i).Item2 >= Deadzone;
                    bool canScrollDown = XController.GetRightStickValue(i).Item2 <= -Deadzone;

                    if (Enabled)
                    {
                        if (EnabledRuntime && (pressed == LockUIControl))
                        {
                            EnabledRuntime = false;
                            Globals.MainOSD.Show(1600, $"UI Control Locked");
                            Thread.Sleep(800);
                        }
                        else if (!EnabledRuntime && (pressed == LockUIControl))
                        {
                            EnabledRuntime = true;
                            Globals.MainOSD.Show(1600, $"UI Control Unlocked");
                            Thread.Sleep(800);
                        }
                    }

                    if (!EnabledRuntime)
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    if (canScrollUp)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, cursor.X, cursor.Y, scrollStep * ScrollUpSpeed, 0);///Mouse wheel Up
                    }
                    else if (canScrollDown)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, cursor.X, cursor.Y, scrollStep * ScrollDownSpeed, 0);///Mouse wheel Down
                    }

                    if (canMouveRight)
                    {
                        x += steps * MouveRightSpeed;
                    }

                    if (canMouveLeft)
                    {
                        x -= steps * MouveLeftSpeed;
                    }

                    if (canMouveUp)
                    {
                        y += steps * MouveUpSpeed;
                    }

                    if (canMouveDown)
                    {
                        y -= steps * MouveDownSpeed;
                    }

                    ///Set cursor position accordingly to the possibilities and values
                    if (canMouveRight || canMouveLeft || canMouveUp || canMouveDown)
                    {
                        SetCursorPos(x, y);
                    }

                    if ((pressed == LeftClick || rt == LeftClick || lt == LeftClick) && prevPressed != pressed)///Left click and release(single click) 
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, cursor.X, cursor.Y, 0, 0);///Left Mouse Button Down
                        Thread.Sleep(200);
                        mouse_event(MOUSEEVENTF_LEFTUP, cursor.X, cursor.Y, 0, 0);///Right Mouse Button Up
                        dragging = false;
                    }

                    if ((pressed == RightClick || rt == RightClick || lt == RightClick) && prevPressed != pressed)///Right click and release(single click)
                    {
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, cursor.X, cursor.Y, 0, 0);///Right Mouse Button Down
                        Thread.Sleep(200);
                        mouse_event(MOUSEEVENTF_RIGHTUP, cursor.X, cursor.Y, 0, 0);///Right Mouse Button Up
                        dragging = false;
                    }

                    if ((pressed == Dragdrop || rt == Dragdrop || lt == Dragdrop) && pressed != prevPressed && !dragging)///Left click //catch/drag  
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, cursor.X, cursor.Y, 0, 0);
                        dragging = true;
                        Thread.Sleep(200);
                    }
                    else if ((pressed == Dragdrop || rt == Dragdrop || lt == Dragdrop) && pressed != prevPressed && dragging)///Left click //release 
                    {
                        mouse_event(MOUSEEVENTF_LEFTUP, cursor.X, cursor.Y, 0, 0);
                        dragging = false;
                        Thread.Sleep(200);
                    }
                    else if (pressed == OpenOsk || rt == OpenOsk || lt == OpenOsk)///Open/close Onscreen Might have to focus nc window before opening need more testing
                    {                   
                        if (!oskCleared)///kill osk if running before nucleus startup
                        {
                            KillOsk();
                            oskCleared = true;
                        }

                        if (!oskVisible)
                        {
                            GlobalWindowMethods.ChangeForegroundWindow();///need to focus nucleus window in order to toggle the osk
                            _OpenOsk();
                            Globals.MainOSD.Show(1600, $"On Screen Keyboard Opened");
                            Thread.Sleep(1100);
                            oskVisible = true;
                        }
                        else
                        {
                            KillOsk();
                            Globals.MainOSD.Show(1600, $"On Screen Keyboard Closed");
                            Thread.Sleep(500);
                            oskVisible = false;
                        }
                    }

                    prevPressed = pressed;
                }

                Thread.Sleep(pollRate);
            }
        }

        private static void KillOsk()
        {
            Process[] _oskProc = Process.GetProcessesByName("TabTip");
            for (int p = 0; p < _oskProc.Length; p++)
            {
                _oskProc[p].Kill();
            }

            Process[] tihProc = Process.GetProcessesByName("TextInputHost");
            for (int p = 0; p < tihProc.Length; p++)
            {
                tihProc[p].Kill();
            }

            oskCleared = true;
        }

        private static void _OpenOsk()
        {   //Need to check if %ProgramW6432% works for all systems (x86 x64)
            string progFiles = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\\Common Files\\Microsoft Shared\\ink");
            string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
            Process.Start(keyboardPath);
        }

        public static void UpdateUINavSettings()
        {
            _Enabled = bool.Parse(ini.IniReadValue("XUINav", "Enabled"));
            Deadzone = int.Parse(ini.IniReadValue("XUINav", "Deadzone"));
            Dragdrop = int.Parse(ini.IniReadValue("XUINav", "DragDrop"));
            RightClick = int.Parse(ini.IniReadValue("XUINav", "RightClick"));
            LeftClick = int.Parse(ini.IniReadValue("XUINav", "LeftClick"));

            if (ini.IniReadValue("XUINav", "LockUIControl").Contains('+'))
            {
                string[] str = ini.IniReadValue("XUINav", "LockUIControl").Split('+');
                LockUIControl = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XUINav", "LockUIControl", "");
            }

            if (ini.IniReadValue("XUINav", "OpenOsk").Contains('+'))
            {
                string[] str = ini.IniReadValue("XUINav", "OpenOsk").Split('+');
                OpenOsk = Convert.ToInt32(str[0]) + Convert.ToInt32(str[1]);
            }
            else
            {
                ini.IniWriteValue("XUINav", "OpenOsk", "");
            }
            EnabledRuntime = _Enabled;
        }
    }

    public static class ControllersShortcuts
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

        public static Thread ctrlsShortcuts;
        public static Thread CtrlsShortcuts => ctrlsShortcuts;
        private static State previousState;

        /// <summary>
        /// Controller shortcuts handling thread.
        /// </summary>
        public static void StartSRTCThread()
        {
            while (true)
            {
                for (int i = 0; i < XController.Controllers.Length; i++)
                {
                   
                    if (!XController.Controllers[i].IsConnected)
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

                    State currentState = (State)XController.GetControllerState(i);

                    if (previousState.PacketNumber != currentState.PacketNumber)
                    {
                        int button = XController.GetPressedButtons(i);
                        int rt = XController.GetRightTriggerValue(i) > 0 ? button + RT : RT;///return RT + button or RT
                        int lt = XController.GetLeftTriggerValue(i) > 0 ? button + LT : LT;///return LT + button or LT

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
                            //Todo Try by pausing focus loop instead so we can release the cursor from it's current window(what for protoinput hooks?)
                            SendKeys.SendWait("%{TAB}");
                            Thread.Sleep(500);
                        }

                        Thread.Sleep(135);
                        previousState = currentState;
                        Pressed = button;
                    }

                    RightTriggerValue = XController.GetRightTriggerValue(i);
                    LeftTriggerValue = XController.GetLeftTriggerValue(i);
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
