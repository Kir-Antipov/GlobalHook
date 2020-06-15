using GlobalHook.Core.Keyboard;
using System;

namespace GlobalHook.Core.Windows.Keyboard
{
    internal class KeyboardEventArgs : IKeyboardEventArgs
    {
        public Keys Key { get; }
        public int RawKey => (int)(Key & Keys.KeyCode);
        public KeyState KeyState { get; }
        public char? KeyChar { get; }

        public bool DefaultPrevented { get; private set; }
        public bool CanPreventDefault { get; }

        public DateTime Time { get; }

        public KeyboardEventArgs(Keys key, KeyState keyState, char? keyChar, DateTime time, bool canPreventDefault = true)
        {
            Key = key;
            KeyState = keyState;
            KeyChar = keyChar;
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
