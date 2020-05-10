namespace GlobalHook.Core.Keyboard
{
    public interface IKeyboardHook : IHook
    {
        event HookEventHandler<IHookEventArgs>? IHook.OnEvent { add => OnEvent += value; remove => OnEvent -= value; }

        new event HookEventHandler<IKeyboardEventArgs>? OnEvent;
    }
}
