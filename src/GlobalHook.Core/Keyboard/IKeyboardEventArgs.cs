namespace GlobalHook.Core.Keyboard
{
    /// <summary>
    /// Represents the base interface for classes that contain keyboard hook event data.
    /// </summary>
    public interface IKeyboardEventArgs : IHookEventArgs
    {
        /// <summary>
        /// Key whose state change caused an event raising.
        /// </summary>
        Keys Key { get; }

        /// <summary>
        /// Raw keycode.
        /// </summary>
        int RawKey { get; }

        /// <summary>
        /// Indicates current key state.
        /// </summary>
        KeyState KeyState { get; }

        /// <summary>
        /// If event was raised by character key, gets the character value.
        /// </summary>
        char? KeyChar { get; }

        HookType IHookEventArgs.HookType => HookType.Keyboard;
    }
}
