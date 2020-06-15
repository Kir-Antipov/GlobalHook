using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using System;
using System.Diagnostics;

namespace GlobalHook.Core.Windows
{
    public abstract class HookBase : IHook
    {
        public abstract HookType HookType { get; }

        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        public bool CanPreventDefault => true;

        public abstract bool CanBeInstalledIntoProcess { get; }

        public bool IsInstalled => Hook is { };

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

            if (IsInstalled)
                ExceptionHelper.ThrowHookIsAlreadyInstalled();

            if (processId != 0 && !CanBeInstalledIntoProcess)
                ExceptionHelper.ThrowHookMustBeGlobal();

            if (!ignoreProcessHasNoWindow)
                ExceptionHelper.ThrowIfProcessHasNoWindow();

            IntPtr moduleHandle;
            int threadId;
            if (processId == 0)
            {
                if (User32.Handle == IntPtr.Zero)
                    ExceptionHelper.ThrowLibraryWasNotLoaded(User32.LibraryName);

                (moduleHandle, threadId) = (User32.Handle, 0);
            }
            else
            {
                Process process = Process.GetProcessById((int)processId);
                (moduleHandle, threadId) = (Kernel32.GetModuleHandle(process.MainModule.ModuleName), User32.GetWindowThreadProcessId(process.MainWindowHandle, out _));
            }

            Hook = LowLevelHook;

            HookHandle = User32.SetWindowsHookEx(HookId, Hook, moduleHandle, threadId);
            if (HookHandle == IntPtr.Zero)
            {
                Hook = null;
                ExceptionHelper.ThrowLastWin32Error((code, msg) => $"Failed to adjust {GetType().Name} for process #{processId}. Error {code}: {msg}");
            }
        }

        public virtual void Uninstall()
        {
            if (!IsInstalled)
                return;

            User32.UnhookWindowsHookEx(HookHandle);
            HookHandle = IntPtr.Zero;
            Hook = null;
        }

        protected abstract IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam);

        protected void InvokeOnEvent(object sender, IHookEventArgs e) => OnEvent?.Invoke(sender, e);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) => Uninstall();

        ~HookBase() => Dispose(false);
    }
}
