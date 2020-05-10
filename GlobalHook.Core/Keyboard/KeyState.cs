namespace GlobalHook.Core.Keyboard
{
    public enum KeyState
    {
        None    = 0b000000000,
        Down    = 0b100000000,
        Up      = 0b100000001,
        SysDown = 0b100000100,
        SysUp   = 0b100000101
    }

    public static class KeyStateExtensions
    {
        public static bool IsUp(this KeyState state) => state == KeyState.Up || state == KeyState.SysUp;

        public static bool IsDown(this KeyState state) => state == KeyState.Down || state == KeyState.SysDown;
    }
}
