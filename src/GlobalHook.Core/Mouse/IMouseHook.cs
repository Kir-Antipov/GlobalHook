﻿using GlobalHook.Core.Extensions;
using System;

namespace GlobalHook.Core.Mouse
{
    /// <summary>
    /// Represents the base interface for mouse hooks.
    /// </summary>
    public interface IMouseHook : IHook
    {
        HookType IHook.HookType => HookType.Mouse;

        /// <inheritdoc cref="IHook.OnEvent"/>
        new event EventHandler<IMouseEventArgs>? OnEvent
        {
            add => ((IHook)this).OnEvent += value.Cast<EventHandler<IHookEventArgs>>();
            remove => ((IHook)this).OnEvent -= value.Cast<EventHandler<IHookEventArgs>>();
        }

        /// <summary>
        /// Occurs when a mouse button is pressed. 
        /// </summary>
        event EventHandler<IMouseEventArgs>? MouseDown;

        /// <summary>
        /// Occurs when a mouse button is released. 
        /// </summary>
        event EventHandler<IMouseEventArgs>? MouseUp;

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
        event EventHandler<IMouseEventArgs>? MouseClick;

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
        event EventHandler<IMouseEventArgs>? MouseDoubleClick;

        /// <summary>
        /// Occurs when a mouse wheel rotates. 
        /// </summary>
        event EventHandler<IMouseEventArgs>? MouseWheel;

        /// <summary>
        /// Occurs when a mouse horizontal scroll wheel is tilted or rotated.
        /// </summary>
        event EventHandler<IMouseEventArgs>? MouseHorizontalWheel;

        /// <summary>
        /// Occurs when the mouse moves. 
        /// </summary>
        event EventHandler<IMouseEventArgs>? MouseMove;
    }
}
