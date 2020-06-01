﻿using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using GlobalHook.Core.Windows.Interop.Structures;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace GlobalHook.Core.Windows.Mouse
{
    public class RawMouseHook : IMouseHook
    {
        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        private int LastLeftClick = 0;

        private RawHook? Hook = null;
        private IntPtr Window = IntPtr.Zero;

        public virtual void Install(bool ignoreProcessHasNoWindow = false) => Install(0);

        public virtual void Install(long processId, bool ignoreProcessHasNoWindow = false)
        {
            if (!CanBeInstalled)
                throw new PlatformNotSupportedException();

            if (Hook is { })
                return;

            if (processId != 0)
                ExceptionHelper.ThrowHookMustBeGlobal();

            if (!ignoreProcessHasNoWindow)
                ExceptionHelper.ThrowIfProcessHasNoWindow();

            Hook = LowLevelHook;

            string name = Guid.NewGuid().ToString();
            WindowClasses windowClass = new WindowClasses
            {
                Hook = Hook,
                Module = Kernel32.GetModuleHandle(null),
                ClassName = $"{name} Class",
            };

            ushort classId = User32.RegisterClassW(windowClass);
            if (classId == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            Window = User32.CreateWindowExW(0, windowClass.ClassName, name, 0, 0, 0, 0, 0, new IntPtr(-3), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (Window == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            bool registered = User32.RegisterRawInputDevices(new[] { new RawInputDevice { UsagePage = 0x01, Usage = 0x02, Flags = 0x00000100, WindowHandle = Window } }, 1, Marshal.SizeOf<RawInputDevice>());
            if (!registered)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public void Uninstall()
        {
            if (Hook is null)
                return;

            User32.DestroyWindow(Window);
            Window = IntPtr.Zero;
            Hook = null;
        }

        protected virtual IntPtr LowLevelHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            DateTime time = DateTime.Now;

            if (msg != 0x00FF)
                return User32.DefWindowProc(hWnd, msg, wParam, lParam);

            int size = Marshal.SizeOf<RawInput>();
            size = User32.GetRawInputData(lParam, 0x10000003, out RawInput raw, ref size, Marshal.SizeOf<RawInputHeader>());
            if (size == -1 || raw.Header.Type != 0)
                return IntPtr.Zero;

            Handle(time, raw.Mouse.Data.Flags, raw.Mouse);

            return IntPtr.Zero;
        }

        private void Handle(DateTime time, RawMouseButtons state, RawInputMouse data)
        {
            IPoint coords = new Point(data.X, data.Y, !data.Flags.HasFlag(RawMouseFlags.MoveAbsolute));

            // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-mousewheel
            const int wheelDelta = 120;
            int delta = data.Data.Delta / wheelDelta;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => Uninstall();

        ~RawMouseHook() => Dispose(false);

        public event EventHandler<IHookEventArgs>? OnEvent;
        public event EventHandler<IMouseEventArgs>? MouseDown;
        public event EventHandler<IMouseEventArgs>? MouseUp;
        public event EventHandler<IMouseEventArgs>? MouseClick;
        public event EventHandler<IMouseEventArgs>? MouseDoubleClick;
        public event EventHandler<IMouseEventArgs>? MouseWheel;
        public event EventHandler<IMouseEventArgs>? MouseHorizontalWheel;
        public event EventHandler<IMouseEventArgs>? MouseMove;
    }
}
