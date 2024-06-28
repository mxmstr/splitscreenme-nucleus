using Nucleus.Gaming.Coop.ProtoInput;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Nucleus.Gaming.Coop.InputManagement
{
    public static class LockInput
    {
        private static bool isLocking = false;

        public static bool IsLocked { get; private set; }

        public static void Lock(bool suspendExplorer, bool freezeExternalInputWhenInputNotLocked, ProtoInputOptions protoOptions)
        {
            if (isLocking)
            {
                return;
            }
            else
            {
                isLocking = true;
            }


            //InputInterceptor.InterceptEnabled = true;


            //System.Windows.Forms.Cursor.Hide();

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(0, 0);
            System.Windows.Forms.Cursor.Clip = new System.Drawing.Rectangle(0, 0, 1, 1);

            //WinApi.SetForegroundWindow(WinApi.GetDesktopWindow());

            ProtoInput.ProtoInput.protoInput.LockInput(true);

            if (suspendExplorer)
            {
                ProtoInput.ProtoInput.protoInput.SuspendExplorer();
            }

            Debug.WriteLine("Locked input, suspended explorer = " + suspendExplorer);

            isLocking = false;
            IsLocked = true;

            if (freezeExternalInputWhenInputNotLocked)
            {
                ProtoInputLauncher.NotifyInputLockChange();
            }

            protoOptions?.OnInputLocked?.Invoke();
        }

        public static void Unlock(bool freezeExternalInputWhenInputNotLocked, ProtoInputOptions protoOptions)
        {
            if (isLocking)
            {
                return;
            }
            else
            {
                isLocking = true;
            }

            //System.Windows.Forms.Cursor.Show();
            System.Windows.Forms.Cursor.Clip = new System.Drawing.Rectangle();

            //InputInterceptor.InterceptEnabled = false;

            ProtoInput.ProtoInput.protoInput.LockInput(false);

            ProtoInput.ProtoInput.protoInput.RestartExplorer();

            Debug.WriteLine("Unlocked input");

            isLocking = false;
            IsLocked = false;

            if (freezeExternalInputWhenInputNotLocked)
            {
                ProtoInputLauncher.NotifyInputLockChange();
            }

            protoOptions?.OnInputUnlocked?.Invoke();
        }

        private static IDictionary<string, int> lockKeys = new Dictionary<string, int>
                {
                    { "End", 0x23 },
                    { "Home", 0x24 },
                    { "Delete", 0x2E },
                    { "Multiply", 0x6A },
                    { "F1", 0x70 },
                    { "F2", 0x71 },
                    { "F3", 0x72 },
                    { "F4", 0x73 },
                    { "F5", 0x74 },
                    { "F6", 0x75 },
                    { "F7", 0x76 },
                    { "F8", 0x77 },
                    { "F9", 0x78 },
                    { "F10", 0x79 },
                    { "F11", 0x7A },
                    { "F12", 0x7B },
                    { "+", 0xBB },
                    { "-", 0xBD },
                    { "Numpad 0", 0x60 },
                    { "Numpad 1", 0x61 },
                    { "Numpad 2", 0x62 },
                    { "Numpad 3", 0x63 },
                    { "Numpad 4", 0x64 },
                    { "Numpad 5", 0x65 },
                    { "Numpad 6", 0x66 },
                    { "Numpad 7", 0x67 },
                    { "Numpad 8", 0x68 },
                    { "Numpad 9", 0x69 }
        };

        public static int GetLockKey()
        {
            IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
            string lockKey = ini.IniReadValue("Hotkeys", "LockKey");

            foreach (KeyValuePair<string, int> key in lockKeys)
            {
                if (key.Key != lockKey)
                {
                    continue;
                }

                return key.Value;
            }

            return 0x23;//End
        }
    }
}
