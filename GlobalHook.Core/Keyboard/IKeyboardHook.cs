namespace GlobalHook.Core.Keyboard
{
    public interface IKeyboardHook : IHook
    {
        event HookEventHandler<IHookEventArgs>? IHook.OnEvent { add => OnEvent += value; remove => OnEvent -= value; }

        new event HookEventHandler<IKeyboardEventArgs>? OnEvent;

        /// <summary>
        /// Occurs when a key is pressed. 
        /// </summary>
        event HookEventHandler<IKeyboardEventArgs>? KeyDown;

        /// <summary>
        /// Occurs when a key is released. 
        /// </summary>
        event HookEventHandler<IKeyboardEventArgs>? KeyUp;

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <remarks>
        /// Key events occur in the following order: 
        /// <list type="number">
        /// <item><see cref="KeyDown"/></item>
        /// <item><see cref="KeyPress"/></item>
        /// <item><see cref="KeyUp"/></item>
        /// </list>
        /// The <see cref="KeyPress"/> event is not raised by noncharacter keys; however, the noncharacter keys do raise 
        /// the <see cref="KeyDown"/> and <see cref="KeyUp"/> events. 
        /// Use the <see cref="IKeyboardEventArgs.KeyChar"/> property to sample keystrokes at run time and to consume a subset of common keystrokes. 
        /// </remarks>
        event HookEventHandler<IKeyboardEventArgs>? KeyPress;
    }
}
