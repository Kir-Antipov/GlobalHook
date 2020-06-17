using GlobalHook.Core.Keyboard;

namespace GlobalHook.Core.Mouse
{
    /// <summary>
    /// Represents the base interface for classes that contain mouse hook event data.
    /// </summary>
    public interface IMouseEventArgs : IHookEventArgs
    {
        /// <summary>
        /// Indicates which mouse action caused an event raising.
        /// </summary>
        MouseEventType MouseEventType { get; }

        /// <summary>
        /// Cursor position or movement (see <see cref="IPoint.IsRelative"/>). 
        /// </summary>
        IPoint Coords { get; }

        /// <summary>
        /// Wheel delta.
        /// </summary>
        int Delta { get; }

        /// <summary>
        /// Indicates which mouse button was pressed or released.
        /// </summary>
        MouseButtons Key { get; }

        /// <summary>
        /// Indicates current mouse button state.
        /// </summary>
        KeyState KeyState { get; }

        /// <summary>
        /// If <para/>
        /// <see cref="MouseEventType"/> == <see cref="MouseEventType.Key"/>,<para/>
        /// <see cref="Key"/> == <see cref="MouseButtons.Left"/> and<para/>
        /// <see cref="KeyState"/> == <see cref="KeyState.Down"/>,<para/>
        /// then this property indicates whether the click was second in a row.
        /// </summary>
        bool IsDoubleClick { get; }

        HookType IHookEventArgs.HookType => HookType.Mouse;
    }
}
