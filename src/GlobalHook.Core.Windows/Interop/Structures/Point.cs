using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
    }
}
