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
    /// <summary>
    /// Windows mouse hook.
    /// </summary>
    /// <remarks>
    /// This hook can only be applied globally.<para/>
    /// Movements are recorded in absolute values, preventing of default actions is possible.
    /// </remarks>
    public class MouseHook : HookBase, IMouseHook
    {
        /// <inheritdoc cref="HookBase.HookType"/>
        public override HookType HookType => HookType.Mouse;

        /// <inheritdoc cref="HookBase.CanBeInstalledIntoProcess"/>
        public override bool CanBeInstalledIntoProcess => false;

        private int LastLeftClick = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseHook"/> class.
        /// </summary>
        public MouseHook() : base(HookId.Mouse) { }

        /// <inheritdoc cref="HookBase.LowLevelHook"/>
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
            int delta = data.WheelDelta;
            DateTime time = data.DateTime;

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

        /// <inheritdoc cref="IMouseHook.MouseDown"/>
        public event EventHandler<IMouseEventArgs>? MouseDown;

        /// <inheritdoc cref="IMouseHook.MouseUp"/>
        public event EventHandler<IMouseEventArgs>? MouseUp;

        /// <inheritdoc cref="IMouseHook.MouseClick"/>
        public event EventHandler<IMouseEventArgs>? MouseClick;

        /// <inheritdoc cref="IMouseHook.MouseDoubleClick"/>
        public event EventHandler<IMouseEventArgs>? MouseDoubleClick;

        /// <inheritdoc cref="IMouseHook.MouseWheel"/>
        public event EventHandler<IMouseEventArgs>? MouseWheel;

        /// <inheritdoc cref="IMouseHook.MouseHorizontalWheel"/>
        public event EventHandler<IMouseEventArgs>? MouseHorizontalWheel;

        /// <inheritdoc cref="IMouseHook.MouseMove"/>
        public event EventHandler<IMouseEventArgs>? MouseMove;
    }
}
