using GlobalHook.Core.Windows.Interop.Enums;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RawInputMouse
    {
        public RawMouseFlags Flags;
        public RawInputButtons Data;
        public uint RawButtons;
        public int X;
        public int Y;
        public uint ExtraInformation;
    }
}
