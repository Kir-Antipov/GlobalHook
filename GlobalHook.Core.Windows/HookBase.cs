using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using System;

namespace GlobalHook.Core.Windows
{
    public abstract class HookBase : IHook
    {
        public abstract HookType HookType { get; }

        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        public bool Installed => Hook is { };

        private readonly HookId HookId;
        private Hook? Hook = null;
        private IntPtr HookHandle = IntPtr.Zero;

        public event EventHandler<IHookEventArgs>? OnEvent;

        internal HookBase(HookId hookId) => HookId = hookId;
        protected HookBase(int hookId) => HookId = (HookId)hookId;

        public virtual void Install(bool ignoreProcessHasNoWindow = false) => Install(0, ignoreProcessHasNoWindow);

        public virtual void Install(long processId, bool ignoreProcessHasNoWindow = false)
        {
            if (!CanBeInstalled)
                ExceptionHelper.ThrowHookCantBeInstalled();

            if (Installed)
                ExceptionHelper.ThrowHookIsAlreadyInstalled();

            if (processId != 0)
                ExceptionHelper.ThrowHookMustBeGlobal();

            if (!ignoreProcessHasNoWindow)
                ExceptionHelper.ThrowIfProcessHasNoWindow();

            if (User32.Handle == IntPtr.Zero)
                ExceptionHelper.ThrowLibraryWasNotLoaded(User32.LibraryName);

            Hook = LowLevelHook;
            (IntPtr moduleHandle, int threadId) = (User32.Handle, 0);

            HookHandle = User32.SetWindowsHookEx(HookId, Hook, moduleHandle, threadId);
            if (HookHandle == IntPtr.Zero)
            {
                Hook = null;
                ExceptionHelper.ThrowLastWin32Error((code, msg) => $"Failed to adjust {GetType().Name} for process #{processId}. Error {code}: {msg}");
            }
        }

        public virtual void Uninstall()
        {
            if (!Installed)
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
