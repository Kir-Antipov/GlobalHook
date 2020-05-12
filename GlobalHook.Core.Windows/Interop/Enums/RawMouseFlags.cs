using System;

namespace GlobalHook.Core.Windows.Interop.Enums
{
    [Flags]
    internal enum RawMouseFlags : ushort
    {
        MoveRelative        = 0b000,
        MoveAbsolute        = 0b001,
        VirtualDesktop      = 0b010,
        AttributesChanged   = 0b100
    }
}
