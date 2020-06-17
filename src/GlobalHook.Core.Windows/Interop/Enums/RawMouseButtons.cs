using System;

namespace GlobalHook.Core.Windows.Interop.Enums
{
    [Flags]
    internal enum RawMouseButtons : ushort
    {
        None        = 0b000000000000,
        LeftDown    = 0b000000000001,
        LeftUp      = 0b000000000010,
        RightDown   = 0b000000000100,
        RightUp     = 0b000000001000,
        MiddleDown  = 0b000000010000,
        MiddleUp    = 0b000000100000,
        Button4Down = 0b000001000000,
        Button4Up   = 0b000010000000,
        Button5Down = 0b000100000000,
        Button5Up   = 0b001000000000,
        Wheel       = 0b010000000000,
        HWheel      = 0b100000000000
    }
}
