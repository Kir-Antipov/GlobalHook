using GlobalHook.Core.Keyboard;

namespace GlobalHook.Core.Mouse
{
    public interface IMouseEventArgs : IHookEventArgs
    {
        MouseEventType MouseEventType { get; }

        IPoint Coords { get; }

        int Delta { get; }

        MouseButtons Key { get; }
        KeyState KeyState { get; }

        /// <summary>
        /// If <see cref="MouseEventType"/> == <see cref="MouseEventType.Key"/>,
        /// <see cref="Key"/> == <see cref="MouseButtons.Left"/> and
        /// <see cref="KeyState"/> == <see cref="KeyState.Down"/>,
        /// then this property indicates whether the click was second in a row
        /// </summary>
        bool IsDoubleClick { get; }

        SenderType IHookEventArgs.SenderType => SenderType.Mouse;
    }
}
