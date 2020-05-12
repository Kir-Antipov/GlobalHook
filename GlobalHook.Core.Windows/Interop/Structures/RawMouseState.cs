using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RawMouseState
    {
        public RawPoint Point;
        public uint Data;
        public int Flags;
        public int Time;
        public IntPtr ExtraInformation;
    }
}
