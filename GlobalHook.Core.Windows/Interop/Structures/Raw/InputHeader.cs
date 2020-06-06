using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputHeader
    {
        public int Type;
        public int Size;
        public IntPtr Device;
        public IntPtr WParam;
    }
}
