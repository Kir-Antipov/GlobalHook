using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows
{
    public abstract class HookBase : IHook
    {
        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        private readonly HookId HookId;
        private Hook? Hook = null;
        private IntPtr HookHandle = IntPtr.Zero;

        public event EventHandler<IHookEventArgs>? OnEvent;

        internal HookBase(HookId hookId) => HookId = hookId;
        protected HookBase(int hookId) => HookId = (HookId)hookId;

        public virtual void Install(bool ignoreProcessHasNoWindow = false) => Install(0, ignoreProcessHasNoWindow);

        public virtual void Install(long threadId, bool ignoreProcessHasNoWindow = false)
        {
            if (!CanBeInstalled)
                throw new PlatformNotSupportedException();

            if (HookHandle != IntPtr.Zero)
                return;

            if (!ignoreProcessHasNoWindow && Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero)
                throw new NotSupportedException($"This process doesn't provide a built-in message loop.\n\nTo successfully install a hook, use `MessageLoop.Run(hook)`.");

            Hook = LowLevelHook;

            IntPtr moduleHandle = IntPtr.Zero;
            if (threadId == 0)
            {
                if (User32.Handle == IntPtr.Zero)
                    throw new NotSupportedException($"Library '{User32.LibraryName}' is undefined.");
                moduleHandle = User32.Handle;
            }

            HookHandle = User32.SetWindowsHookEx(HookId, Hook, moduleHandle, (int)threadId);
            if (HookHandle == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, $"Failed to adjust {GetType().Name} for thread {threadId}. Error {errorCode}: {new Win32Exception(errorCode).Message}.");
            }
        }

        public virtual void Uninstall()
        {
            if (HookHandle == IntPtr.Zero)
                return;

            User32.UnhookWindowsHookEx(HookHandle);
            HookHandle = IntPtr.Zero;
            Hook = null;
        }

        protected abstract IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam);

        protected void InvokeOnEvent(object sender, IHookEventArgs e) => OnEvent?.Invoke(sender, e);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => Uninstall();

        ~HookBase() => Dispose(false);
    }
}
