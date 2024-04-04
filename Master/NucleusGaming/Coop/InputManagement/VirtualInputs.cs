using System;
using System.Runtime.InteropServices;

namespace Nucleus.Gaming.Coop.InputManagement
{
    public class VirtualInputs
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);


        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        public static void SendKeyboardInput(ushort keyCode)
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                  type = (int)InputType.Keyboard,
                       u = new InputUnion
                       {
                            ki = new KeyboardInput
                            {
                                wVk = keyCode,
                                //wScan = 0x11, // W
                                 wScan = 0,// 0x2c, //W azerty kb
                                dwFlags = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode),
                                dwExtraInfo = GetMessageExtraInfo()
                             }
                        }
                 },

                new Input
                {
                     type = (int)InputType.Keyboard,
                      u = new InputUnion
                      {
                          ki = new KeyboardInput
                          {
                            wVk = keyCode,
                            //wScan = 0x11, // W
                            wScan = 0 ,// 0x2c, //W azerty kb
                            dwFlags = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                          }
                      }
                }
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));

        }

        public static void SendMouseInput()
        {
            Input[] inputs = new Input[]
            {
              new Input
              {

                type = (int) InputType.Mouse,
                 u = new InputUnion

                 {

                   mi = new MouseInput
                   {
                     dx = 100,
                     dy = 100,
                     dwFlags = (uint)(MouseEventF.Move | MouseEventF.LeftDown),
                     dwExtraInfo = GetMessageExtraInfo()
                   }

                 }

               },

              new Input
              {
                type = (int) InputType.Mouse,
                u = new InputUnion

                {

                  mi = new MouseInput
                  {

                    dwFlags = (uint)MouseEventF.LeftUp,
                    dwExtraInfo = GetMessageExtraInfo()

                  }

                }

              }

             };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

    }
}
