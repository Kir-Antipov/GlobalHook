using GlobalHook.Core.Windows.Interop.Enums;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct InputButtons
    {
        [FieldOffset(0)]
        public uint Buttons;

        [FieldOffset(2)]
        public short Delta;

        [FieldOffset(0)]
        public RawMouseButtons Flags;
    }
}
