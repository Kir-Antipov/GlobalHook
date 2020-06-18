using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Libs;
using System;
using System.Diagnostics;

namespace GlobalHook.Core.Windows
{
    /// <summary>
    /// Base class for defining WinAPI hooks using SetWindowsHookEx.
    /// </summary>
    public abstract class HookBase : IHook
    {
        /// <inheritdoc cref="IHook.HookType"/>
        public abstract HookType HookType { get; }

        /// <inheritdoc cref="IHook.CanBeInstalled"/>
        public bool CanBeInstalled => Environment.OSVersion.Platform == PlatformID.Win32NT;

        /// <inheritdoc cref="IHook.CanPreventDefault"/>
        public bool CanPreventDefault => true;

        /// <inheritdoc cref="IHook.CanBeInstalledIntoProcess"/>
        public abstract bool CanBeInstalledIntoProcess { get; }

        /// <inheritdoc cref="IHook.IsInstalled"/>
        public bool IsInstalled => Hook is { };

        private readonly HookId HookId;
        private Hook? Hook = null;
        private IntPtr HookHandle = IntPtr.Zero;

        /// <inheritdoc cref="IHook.OnEvent"/>
        public event EventHandler<IHookEventArgs>? OnEvent;

        internal HookBase(HookId hookId) => HookId = hookId;

        /// <summary>
        /// Using this initializer, you can determine the type of hook used as the first SetWindowsHookEx's parameter.
        /// </summary>
        /// <param name="hookId">Hook type identifier.</param>
        protected HookBase(int hookId) => HookId = (HookId)hookId;

        /// <inheritdoc cref="IHook.Install(bool)"/>
        public virtual void Install(bool ignoreProcessHasNoWindow = false) => Install(0, ignoreProcessHasNoWindow);

        /// <inheritdoc cref="IHook.Install(long, bool)"/>
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

        /// <inheritdoc cref="IHook.Uninstall"/>
        public virtual void Uninstall()
        {
            if (!IsInstalled)
                return;

            User32.UnhookWindowsHookEx(HookHandle);
            HookHandle = IntPtr.Zero;
            Hook = null;
        }

        /// <summary>
        /// Hook handler.
        /// </summary>
        /// <param name="nCode">HookProc nCode.</param>
        /// <param name="wParam">HookProc wParam.</param>
        /// <param name="lParam">HookProc lParam.</param>
        /// <returns>HookProc lResult.</returns>
        protected abstract IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Invokes <see cref="OnEvent"/> if it's not <see langword="null"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains the event data.</param>
        protected void InvokeOnEvent(object sender, IHookEventArgs e) => OnEvent?.Invoke(sender, e);

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <inheritdoc cref="Dispose()"/>
        /// <param name="disposing">Indicates whether this method is called from managed code (<see langword="true"/>).</param>
        protected virtual void Dispose(bool disposing) => Uninstall();

        ~HookBase() => Dispose(false);
    }
}
