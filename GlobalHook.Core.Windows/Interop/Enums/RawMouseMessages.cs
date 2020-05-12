namespace GlobalHook.Core.Windows.Interop.Enums
{
    internal enum RawMouseMessages
    {
        Move        = 0b1000000000,
        LeftDown    = 0b1000000001,
        LeftUp      = 0b1000000010,
        RightDown   = 0b1000000100,
        RightUp     = 0b1000000101,
        MiddleDown  = 0b1000000111,
        MiddleUp    = 0b1000001000,
        Wheel       = 0b1000001010,
        HWheel      = 0b1000001110,
    }
}
