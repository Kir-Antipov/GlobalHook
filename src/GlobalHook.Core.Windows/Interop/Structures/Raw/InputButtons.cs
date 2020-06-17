using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct InputButtons
    {
        [FieldOffset(0)]
        public uint Buttons;

        [FieldOffset(0)]
        public RawMouseButtons Flags;

        public int WheelDelta => ((int)(Buttons & 0xFFFF0000) >> 16) / User32.WheelDelta;
    }
}
