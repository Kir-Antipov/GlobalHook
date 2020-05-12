using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RawPoint
    {
        public int X;
        public int Y;
    }
}
