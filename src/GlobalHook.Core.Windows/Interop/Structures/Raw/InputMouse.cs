using GlobalHook.Core.Windows.Interop.Enums;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputMouse
    {
        public RawMouseFlags Flags;
        public InputButtons Data;
        public uint RawButtons;
        public int X;
        public int Y;
        public uint ExtraInformation;
    }
}
