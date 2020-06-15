using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using System;

namespace GlobalHook.Core.Windows.Mouse
{
    internal class MouseEventArgs : IMouseEventArgs
    {
        public MouseEventType MouseEventType { get; }

        public IPoint Coords { get; }

        public int Delta { get; }

        public MouseButtons Key { get; }
        public KeyState KeyState { get; }
        public bool IsDoubleClick { get; }

        public bool CanPreventDefault { get; }
        public bool DefaultPrevented { get; private set; }

        public DateTime Time { get; }

        public MouseEventArgs(MouseEventType eventType, IPoint coords, int delta, MouseButtons key, KeyState keyState, bool isDoubleClick, DateTime time, bool canPreventDefault = true)
        {
            MouseEventType = eventType;
            Coords = coords;
            Delta = delta;
            Key = key;
            KeyState = keyState;
            IsDoubleClick = isDoubleClick;
            Time = time;
            DefaultPrevented = false;
            CanPreventDefault = canPreventDefault;
        }

        public void PreventDefault()
        {
            if (!CanPreventDefault)
                throw new NotSupportedException();

            DefaultPrevented = true;
        }
    }
}
