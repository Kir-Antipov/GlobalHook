using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RawMessage
    {
        public IntPtr Handler;
        public uint Message;
        public UIntPtr WParam;
        public IntPtr LParam;
        public int Time;
        public RawPoint Point;
    }
}
