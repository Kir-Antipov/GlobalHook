namespace GlobalHook.Core.Keyboard
{
    /// <summary>
    /// Specifies available key states.
    /// </summary>
    public enum KeyState
    {
        /// <summary>
        /// Key state is undefined.
        /// </summary>
        None    = 0b000000000,

        /// <summary>
        /// Key is down.
        /// </summary>
        Down    = 0b100000000,

        /// <summary>
        /// Key is up.
        /// </summary>
        Up      = 0b100000001,

        /// <summary>
        /// System key is down.
        /// </summary>
        SysDown = 0b100000100,

        /// <summary>
        /// System key is up.
        /// </summary>
        SysUp   = 0b100000101
    }

    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for <see cref="KeyState"/> values.
    /// </summary>
    public static class KeyStateExtensions
    {
        /// <summary>
        /// Checks if the given <see cref="KeyState"/> instance represents a key that is currently up.
        /// </summary>
        /// <param name="state"><see cref="KeyState"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if key is up; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsUp(this KeyState state) => state == KeyState.Up || state == KeyState.SysUp;

        /// <summary>
        /// Checks if the given <see cref="KeyState"/> instance represents a key that is currently down.
        /// </summary>
        /// <param name="state"><see cref="KeyState"/> instance.</param>
        /// <returns>
        /// <see langword="true"/> if key is down; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDown(this KeyState state) => state == KeyState.Down || state == KeyState.SysDown;
    }
}
