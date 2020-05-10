namespace GlobalHook.Core.Keyboard
{
    public interface IKeyboardEventArgs : IHookEventArgs
    {
        Keys Key { get; }
        KeyState KeyState { get; }

        SenderType IHookEventArgs.SenderType => SenderType.Keyboard;
    }
}
