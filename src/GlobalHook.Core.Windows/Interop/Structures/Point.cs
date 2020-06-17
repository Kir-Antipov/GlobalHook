using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int X;
        public int Y;
    }
}
