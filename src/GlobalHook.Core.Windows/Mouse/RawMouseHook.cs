using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using GlobalHook.Core.Windows.Interop.Structures;
using GlobalHook.Core.Windows.Interop.Structures.Raw;
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
    /// Movements are recorded in relative values, preventing of default actions is not possible.
    /// </remarks>
    public class RawMouseHook : IMouseHook
    {
        /// <inheritdoc cref="IHook.HookType"/>
        public HookType HookType => HookType.Mouse;

        /// <inheritdoc cref="IHook.CanBeInstalled"/>
        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        /// <inheritdoc cref="IHook.CanBeInstalledIntoProcess"/>
        public bool CanBeInstalledIntoProcess => false;

        /// <inheritdoc cref="IHook.CanPreventDefault"/>
        public bool CanPreventDefault => false;

        /// <inheritdoc cref="IHook.IsInstalled"/>
        public bool IsInstalled => Hook is { };

        private int LastLeftClick = 0;

        private RawHook? Hook = null;
        private IntPtr Window = IntPtr.Zero;

        /// <inheritdoc cref="IHook.Install(bool)"/>
        public virtual void Install(bool ignoreProcessHasNoWindow = false) => Install(0);

        /// <inheritdoc cref="IHook.Install(long, bool)"/>
        public virtual void Install(long processId, bool ignoreProcessHasNoWindow = false)
        {
            if (!CanBeInstalled)
                ExceptionHelper.ThrowHookCantBeInstalled();

            if (IsInstalled)
                ExceptionHelper.ThrowHookIsAlreadyInstalled();

            if (processId != 0)
                ExceptionHelper.ThrowHookMustBeGlobal();

            if (!ignoreProcessHasNoWindow)
                ExceptionHelper.ThrowIfProcessHasNoWindow();

            string name = Guid.NewGuid().ToString();
            WindowClasses windowClass = new WindowClasses
            {
                Hook = RawHook,
                Module = Kernel32.GetModuleHandle(null),
                ClassName = $"{name} Class",
            };

            ushort classId = User32.RegisterClassW(windowClass);
            if (classId == 0)
                ExceptionHelper.ThrowLastWin32Error();

            Window = User32.CreateWindowExW(0, windowClass.ClassName, name, 0, 0, 0, 0, 0, new IntPtr(-3), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (Window == IntPtr.Zero)
                ExceptionHelper.ThrowLastWin32Error();

            bool registered = User32.RegisterRawInputDevices(new[] { new InputDevice { UsagePage = 0x01, Usage = 0x02, Flags = 0x00000100, WindowHandle = Window } }, 1, Marshal.SizeOf<InputDevice>());
            if (!registered)
                ExceptionHelper.ThrowLastWin32Error();

            Hook = windowClass.Hook;
        }

        /// <inheritdoc cref="IHook.Uninstall"/>
        public void Uninstall()
        {
            if (!IsInstalled)
                return;

            User32.DestroyWindow(Window);
            Window = IntPtr.Zero;
            Hook = null;
        }

        /// <summary>
        /// Hook handler.
        /// </summary>
        /// <param name="hWnd">RawHookProc hWnd.</param>
        /// <param name="msg">RawHookProc msg.</param>
        /// <param name="wParam">RawHookProc wParam.</param>
        /// <param name="lParam">RawHookProc lParam.</param>
        /// <returns>RawHookProc lResult.</returns>
        protected virtual IntPtr RawHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            DateTime time = DateTime.Now;

            if (msg != 0x00FF)
                return User32.DefWindowProc(hWnd, msg, wParam, lParam);

            int size = Marshal.SizeOf<Input>();
            size = User32.GetRawInputData(lParam, 0x10000003, out Input raw, ref size, Marshal.SizeOf<InputHeader>());
            if (size == -1 || raw.Header.Type != 0)
                return IntPtr.Zero;

            Handle(time, raw.Mouse.Data.Flags, raw.Mouse);

            return IntPtr.Zero;
        }

        private void Handle(DateTime time, RawMouseButtons state, InputMouse data)
        {
            IPoint coords = new Point(data.X, data.Y, !data.Flags.HasFlag(RawMouseFlags.MoveAbsolute));
            int delta = data.Data.WheelDelta;

            switch (state)
            {
                case RawMouseButtons.None when data.X != 0 || data.Y != 0 || !coords.IsRelative:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Movement, coords, delta, MouseButtons.None, KeyState.None, false, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseMove?.Invoke(this, eventArgs);
                    break;
                }
                case RawMouseButtons.LeftDown:
                {
                    int currentTime = (int)Kernel32.DateTimeToTicks(time);
                    bool isDoubleClick = currentTime - LastLeftClick <= User32.DoubleClickTime;
                    Interlocked.Exchange(ref LastLeftClick, currentTime - (isDoubleClick ? User32.DoubleClickTime + 1 : 0));

                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, MouseButtons.Left, KeyState.Down, isDoubleClick, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseDown?.Invoke(this, eventArgs);
                    MouseClick?.Invoke(this, eventArgs);
                    if (isDoubleClick)
                        MouseDoubleClick?.Invoke(this, eventArgs);
                    break;
                }
                case RawMouseButtons.MiddleDown:
                case RawMouseButtons.RightDown:
                case RawMouseButtons.Button4Down:
                case RawMouseButtons.Button5Down:
                {
                    MouseButtons button = state switch
                    {
                        RawMouseButtons.MiddleDown => MouseButtons.Middle,
                        RawMouseButtons.RightDown => MouseButtons.Right,
                        RawMouseButtons.Button4Down => MouseButtons.XButton1,
                        RawMouseButtons.Button5Down => MouseButtons.XButton2,
                        _ => MouseButtons.None
                    };
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, button, KeyState.Down, false, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseDown?.Invoke(this, eventArgs);
                    MouseClick?.Invoke(this, eventArgs);
                    break;
                }
                case RawMouseButtons.LeftUp:
                case RawMouseButtons.MiddleUp:
                case RawMouseButtons.RightUp:
                case RawMouseButtons.Button4Up:
                case RawMouseButtons.Button5Up:
                {
                    MouseButtons button = state switch
                    {
                        RawMouseButtons.LeftUp => MouseButtons.Left,
                        RawMouseButtons.MiddleUp => MouseButtons.Middle,
                        RawMouseButtons.RightUp => MouseButtons.Right,
                        RawMouseButtons.Button4Up => MouseButtons.XButton1,
                        RawMouseButtons.Button5Up => MouseButtons.XButton2,
                        _ => MouseButtons.None
                    };
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Key, coords, delta, button, KeyState.Up, false, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseUp?.Invoke(this, eventArgs);
                    break;
                }
                case RawMouseButtons.Wheel:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.Wheel, coords, delta, MouseButtons.Wheel, delta > 0 ? KeyState.Up : KeyState.Down, false, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseWheel?.Invoke(this, eventArgs);
                    break;
                }
                case RawMouseButtons.HWheel:
                {
                    IMouseEventArgs eventArgs = new MouseEventArgs(MouseEventType.HorizontalWheel, coords, delta, MouseButtons.Wheel, delta > 0 ? KeyState.Up : KeyState.Down, false, time, false);
                    OnEvent?.Invoke(this, eventArgs);
                    MouseHorizontalWheel?.Invoke(this, eventArgs);
                    break;
                }
                default:
                    break;
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <inheritdoc cref="Dispose()"/>
        /// <param name="disposing">Indicates whether this method is called from managed code (<see langword="true"/>).</param>
        protected virtual void Dispose(bool disposing) => Uninstall();

        ~RawMouseHook() => Dispose(false);

        /// <inheritdoc cref="IHook.OnEvent"/>
        public event EventHandler<IHookEventArgs>? OnEvent;

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
