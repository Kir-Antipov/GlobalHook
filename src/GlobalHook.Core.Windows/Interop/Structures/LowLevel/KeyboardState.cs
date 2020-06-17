using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Windows.Interop.Libs;
using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardState
    {
        public Keys Key;
        public int ScanCode;
        public int Flags;
        public int Time;
        public IntPtr ExtraInformation;

        public DateTime DateTime => Kernel32.TicksToDateTime(Time);
    }
}
