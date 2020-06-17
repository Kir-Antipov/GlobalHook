using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputDevice
    {
        public ushort UsagePage;
        public ushort Usage;
        public int Flags;
        public IntPtr WindowHandle;
    }
}
