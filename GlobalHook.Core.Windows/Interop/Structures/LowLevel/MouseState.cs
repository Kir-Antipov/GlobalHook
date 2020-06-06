using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseState
    {
        public Point Point;
        public uint Data;
        public int Flags;
        public int Time;
        public IntPtr ExtraInformation;
    }
}
