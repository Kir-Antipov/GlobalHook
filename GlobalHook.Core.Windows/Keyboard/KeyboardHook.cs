using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using GlobalHook.Core.Windows.Interop.Structures;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GlobalHook.Core.Windows.Keyboard
{
    public class KeyboardHook : HookBase, IKeyboardHook
    {
        public KeyboardHook() : base(HookId.Keyboard) { }

        protected override IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool prevented = false;
            KeyState state = (KeyState)wParam;
            if (Enum.IsDefined(typeof(KeyState), state))
            {
                RawKeyboardState data = Marshal.PtrToStructure<RawKeyboardState>(lParam);
                prevented = Handle(state, data);
            }
            return prevented ? (IntPtr)1 : User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private bool Handle(KeyState state, RawKeyboardState data)
        {
            Keys key = data.Key;
            char? keyChar = null;

            if (User32.IsKeyPressed(Keys.ControlKey))
                key |= Keys.Control;
            if (User32.IsKeyPressed(Keys.AltKey))
                key |= Keys.Alt;
            if (User32.IsKeyPressed(Keys.ShiftKey) is { } shift && shift)
                key |= Keys.Shift;
            bool capsLock = User32.IsKeyToggled(Keys.CapsLock);

            byte[] keyboardState = new byte[256];
            StringBuilder chars = new StringBuilder(8);
            User32.GetKeyboardState(keyboardState);
            if (User32.ToAscii(data.Key, data.ScanCode, keyboardState, chars, data.Flags) == 1)
                keyChar = shift ^ capsLock ? char.ToUpper(chars[0]) : chars[0];

            IKeyboardEventArgs e = new KeyboardEventArgs(key, state, keyChar, Kernel32.TicksToDateTime(data.Time));
            InvokeOnEvent(this, e);

            switch (state)
            {
                case KeyState.Down:
                case KeyState.SysDown:
                    KeyDown?.Invoke(this, e);
                    KeyPress?.Invoke(this, e);
                    break;
                case KeyState.Up:
                case KeyState.SysUp:
                    KeyUp?.Invoke(this, e);
                    break;
                default:
                    break;
            }

            return e.DefaultPrevented;
        }

        public event EventHandler<IKeyboardEventArgs>? KeyDown;
        public event EventHandler<IKeyboardEventArgs>? KeyUp;
        public event EventHandler<IKeyboardEventArgs>? KeyPress;
    }
}
