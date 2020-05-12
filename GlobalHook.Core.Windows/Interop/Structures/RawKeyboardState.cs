using GlobalHook.Core.Keyboard;
using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RawKeyboardState
    {
        public Keys Key;
        public int ScanCode;
        public int Flags;
        public int Time;
        public IntPtr ExtraInformation;
    }
}
