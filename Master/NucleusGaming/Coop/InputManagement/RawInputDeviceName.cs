using System;
using System.Runtime.InteropServices;

namespace Nucleus.Gaming.Coop.InputManagement
{
    internal sealed class RawInputDeviceName : IDisposable
    {
        private static IntPtr dataDispose;

        public void Dispose()
        {
            Marshal.FreeHGlobal(dataDispose);
        }

        public string GetDeviceName(IntPtr data)
        {
            dataDispose = data;
            return Marshal.PtrToStringAuto(data);
        }
    }
}
