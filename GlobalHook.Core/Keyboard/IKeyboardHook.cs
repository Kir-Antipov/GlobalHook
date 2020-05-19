using GlobalHook.Core.Extensions;
using System;

namespace GlobalHook.Core.Keyboard
{
    public interface IKeyboardHook : IHook
    {
        new event EventHandler<IKeyboardEventArgs>? OnEvent
        {
            add => ((IHook)this).OnEvent += value.Cast<EventHandler<IHookEventArgs>>();
            remove => ((IHook)this).OnEvent -= value.Cast<EventHandler<IHookEventArgs>>();
        }

        /// <summary>
        /// Occurs when a key is pressed. 
        /// </summary>
        event EventHandler<IKeyboardEventArgs>? KeyDown;

        /// <summary>
        /// Occurs when a key is released. 
        /// </summary>
        event EventHandler<IKeyboardEventArgs>? KeyUp;

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
        event EventHandler<IKeyboardEventArgs>? KeyPress;
    }
}
