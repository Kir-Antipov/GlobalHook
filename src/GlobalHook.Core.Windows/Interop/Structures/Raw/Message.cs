using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Message
    {
        public IntPtr Handler;
        public uint Command;
        public UIntPtr WParam;
        public IntPtr LParam;
        public int Time;
        public Point Point;
    }
}
