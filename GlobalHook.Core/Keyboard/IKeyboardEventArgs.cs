namespace GlobalHook.Core.Keyboard
{
    public interface IKeyboardEventArgs : IHookEventArgs
    {
        Keys Key { get; }
        int RawKey { get; }
        KeyState KeyState { get; }

        /// <summary>
        /// If event was raised by character key, gets the character value.
        /// </summary>
        char? KeyChar { get; }

        SenderType IHookEventArgs.SenderType => SenderType.Keyboard;
    }
}
