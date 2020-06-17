using GlobalHook.Core.Windows.Interop.Libs;
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

        public int WheelDelta => ((int)(Data & 0xFFFF0000) >> 16) / User32.WheelDelta;
        public DateTime DateTime => Kernel32.TicksToDateTime(Time);
    }
}
