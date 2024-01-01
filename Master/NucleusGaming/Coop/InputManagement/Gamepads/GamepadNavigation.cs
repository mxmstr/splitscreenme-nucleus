using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.InputManagement.Gamepads
{
    public static class GamepadNavigation
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

        public static Thread GamepadNavigationThread;

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
        private static bool oskVisible;

        public static void GamepadNavigationUpdate()
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
                for (int i = 0; i < GamepadState.Controllers.Length; i++)
                {
                    if (!GamepadState.Controllers[i].IsConnected)
                    {
                        continue;
                    }

                    GetCursorPos(out POINT cursor);
                    x = cursor.X;
                    y = cursor.Y;

                    int pressed = GamepadState.GetPressedButtons(i);/// Current pressed Xinput button
                    int rt = GamepadState.GetRightTriggerValue(i) > 0 ? pressed + RT : RT;//return RT + button or RT
                    int lt = GamepadState.GetLeftTriggerValue(i) > 0 ? pressed + LT : LT;//return LT + button or LT

                    ///Adjust the cursor speed to the joystick value
                    int MouveLeftSpeed = (Math.Abs((GamepadState.GetLeftStickValue(i).Item1) / 2000));
                    int MouveRightSpeed = ((GamepadState.GetLeftStickValue(i).Item1) / 2000);
                    int MouveUpSpeed = (Math.Abs((GamepadState.GetLeftStickValue(i).Item2) / 2000));
                    int MouveDownSpeed = ((GamepadState.GetLeftStickValue(i).Item2) / 2000);

                    ///Check if the right joystick values are out of the deadzone and allow to move the cursor or not
                    bool canMouveLeft = GamepadState.GetLeftStickValue(i).Item1 <= -Deadzone;
                    bool canMouveRight = GamepadState.GetLeftStickValue(i).Item1 >= Deadzone;
                    bool canMouveUp = GamepadState.GetLeftStickValue(i).Item2 <= -Deadzone;
                    bool canMouveDown = GamepadState.GetLeftStickValue(i).Item2 >= Deadzone;

                    ///Adjust scrolling speed to the joystick value
                    int ScrollUpSpeed = Math.Abs((GamepadState.GetRightStickValue(i).Item2) / 1000);
                    int ScrollDownSpeed = (GamepadState.GetRightStickValue(i).Item2) / 1000;

                    ///Check if the left joystick value(Y axis only) is out of the deadzone and allow to scroll Up or Down or not
                    bool canScrollUp = GamepadState.GetRightStickValue(i).Item2 >= Deadzone;
                    bool canScrollDown = GamepadState.GetRightStickValue(i).Item2 <= -Deadzone;

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

                        if (!oskVisible)
                        {
                            ToggleOsk();
                            Thread.Sleep(1100);
                            oskVisible = true;
                        }
                        else
                        {
                            ToggleOsk();
                            Thread.Sleep(500);
                            oskVisible = false;
                        }
                    }

                    prevPressed = pressed;
                }

                Thread.Sleep(pollRate);
            }
        }

        private static void ToggleOsk()
        {
            if (!OnScreenKeyboard.IsOpen())
            {
                Globals.MainOSD.Show(1600, $"On Screen Keyboard Opened");
                OnScreenKeyboard.Show();
            }
            else
            {
                Globals.MainOSD.Show(1600, $"On Screen Keyboard Closed");
                OnScreenKeyboard.Hide();
            }
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
}
