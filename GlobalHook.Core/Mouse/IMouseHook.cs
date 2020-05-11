namespace GlobalHook.Core.Mouse
{
    public interface IMouseHook : IHook
    {
        event HookEventHandler<IHookEventArgs>? IHook.OnEvent { add => OnEvent += value; remove => OnEvent -= value; }

        new event HookEventHandler<IMouseEventArgs>? OnEvent;

        /// <summary>
        /// Occurs when the mouse a mouse button is pressed. 
        /// </summary>
        event HookEventHandler<IMouseEventArgs>? MouseDown;

        /// <summary>
        /// Occurs when a mouse button is released. 
        /// </summary>
        event HookEventHandler<IMouseEventArgs>? MouseUp;

        /// <summary>
        /// Occurs when a click was performed by the mouse. 
        /// </summary>
        /// <remarks>
        /// Mouse events occur in the following order: 
        /// <list type="number">
        /// <item><see cref="MouseDown"/></item>
        /// <item><see cref="MouseClick"/></item>
        /// <item><see cref="MouseUp"/></item>
        /// </list>
        /// </remarks>
        event HookEventHandler<IMouseEventArgs>? MouseClick;

        /// <summary>
        /// Occurs when a left click was second in a row. 
        /// </summary>
        /// <remarks>
        /// Mouse events occur in the following order: 
        /// <list type="number">
        /// <item><see cref="MouseDown"/></item>
        /// <item><see cref="MouseClick"/></item>
        /// <item><see cref="MouseDoubleClick"/></item>
        /// <item><see cref="MouseUp"/></item>
        /// </list>
        /// </remarks>
        event HookEventHandler<IMouseEventArgs>? MouseDoubleClick;

        /// <summary>
        /// Occurs when the mouse wheel moves. 
        /// </summary>
        event HookEventHandler<IMouseEventArgs>? MouseWheel;

        /// <summary>
        /// Occurs when the mouse's horizontal scroll wheel is tilted or rotated
        /// </summary>
        event HookEventHandler<IMouseEventArgs>? MouseHorizontalWheel;

        /// <summary>
        /// Occurs when the mouse pointer is moved. 
        /// </summary>
        event HookEventHandler<IMouseEventArgs>? MouseMove;
    }
}
