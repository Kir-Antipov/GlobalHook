using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using GlobalHook.Core.Windows.Interop.Structures.LowLevel;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace GlobalHook.Core.Windows.Mouse
{
    public class MouseHook : HookBase, IMouseHook
    {
        public override HookType HookType => HookType.Mouse;

        public override bool CanBeInstalledIntoProcess => false;

        private int LastLeftClick = 0;

        public MouseHook() : base(HookId.Mouse) { }

        protected override IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool prevented = false;
            RawMouseMessages state = (RawMouseMessages)wParam;
            if (Enum.IsDefined(typeof(RawMouseMessages), state))
            {
                MouseState data = Marshal.PtrToStructure<MouseState>(lParam);
                prevented = Handle(state, data);
            }
            return prevented ? (IntPtr)1 : User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private bool Handle(RawMouseMessages state, MouseState data)
        {
            bool prevented = false;

            IPoint coords = new Point(data.Point.X, data.Point.Y);

            // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-mousewheel
            const int wheelDelta = 120;
            Span<byte> deltaBytes = stackalloc byte[4];
            deltaBytes[2] = (byte)((data.Data & 0x00FF0000) >> 16);
            deltaBytes[3] = (byte)((data.Data & 0xFF000000) >> 24);
            int delta = (BitConverter.ToInt32(deltaBytes) >> 16) / wheelDelta;

            DateTime time = Kernel32.TicksToDateTime(data.Time);

            switch (state)
            {
                case RawMouseMessages.Move:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Movement, coords, delta, MouseButtons.None, KeyState.None, false, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseMove?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                case RawMouseMessages.LeftDown:
                {
                    bool isDoubleClick = (data.Time - LastLeftClick) <= User32.DoubleClickTime;
                    Interlocked.Exchange(ref LastLeftClick, data.Time - (isDoubleClick ? User32.DoubleClickTime + 1 : 0));

                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, MouseButtons.Left, KeyState.Down, isDoubleClick, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseDown?.Invoke(this, eventArgs);
                    MouseClick?.Invoke(this, eventArgs);
                    if (isDoubleClick)
                        MouseDoubleClick?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                case RawMouseMessages.MiddleDown:
                case RawMouseMessages.RightDown:
                {
                    MouseButtons button = state == RawMouseMessages.RightDown ? MouseButtons.Right : MouseButtons.Middle;
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, button, KeyState.Down, false, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseDown?.Invoke(this, eventArgs);
                    MouseClick?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                case RawMouseMessages.LeftUp:
                case RawMouseMessages.MiddleUp:
                case RawMouseMessages.RightUp:
                {
                    MouseButtons button = state == RawMouseMessages.LeftUp ? MouseButtons.Left : state == RawMouseMessages.MiddleUp ? MouseButtons.Middle : MouseButtons.Right;
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, button, KeyState.Up, false, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseUp?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                case RawMouseMessages.Wheel:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Wheel, coords, delta, MouseButtons.Wheel, delta > 0 ? KeyState.Up : KeyState.Down, false, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseWheel?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                case RawMouseMessages.HWheel:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.HorizontalWheel, coords, delta, MouseButtons.Wheel, delta > 0 ? KeyState.Up : KeyState.Down, false, time);
                    InvokeOnEvent(this, eventArgs);
                    MouseHorizontalWheel?.Invoke(this, eventArgs);
                    prevented = eventArgs.DefaultPrevented;
                    break;
                }
                default:
                    break;
            }

            return prevented;
        }

        public event EventHandler<IMouseEventArgs>? MouseDown;
        public event EventHandler<IMouseEventArgs>? MouseUp;
        public event EventHandler<IMouseEventArgs>? MouseClick;
        public event EventHandler<IMouseEventArgs>? MouseDoubleClick;
        public event EventHandler<IMouseEventArgs>? MouseWheel;
        public event EventHandler<IMouseEventArgs>? MouseHorizontalWheel;
        public event EventHandler<IMouseEventArgs>? MouseMove;
    }
}
