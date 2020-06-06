using GlobalHook.Core.Windows.Interop.Libs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows
{
    internal class ExceptionHelper
    {
        public static void ThrowIfProcessHasNoWindow()
        {
            IntPtr currentProcessWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            
            if (currentProcessWindowHandle == IntPtr.Zero || currentProcessWindowHandle == Kernel32.GetConsoleWindow())
                throw new NotSupportedException($"This process doesn't provide a built-in message loop.\n\nTo successfully install a hook, use `MessageLoop.Run(hook)`.");
        }

        public static void ThrowHookMustBeGlobal() => throw new NotSupportedException("Low level hook can't be installed for one specific thread/process.");

        public static void ThrowHookIsAlreadyInstalled() => throw new InvalidOperationException("The hook is already installed.");

        public static void ThrowLastWin32Error() => throw new Win32Exception(Marshal.GetLastWin32Error());

        public static void ThrowLastWin32Error(Func<int, string, string> messageGenerator)
        {
            int errorCode = Marshal.GetLastWin32Error();
            string defaultMessage = new Win32Exception(errorCode).Message;
            throw new Win32Exception(errorCode, messageGenerator(errorCode, defaultMessage));
        }

        public static void ThrowLibraryWasNotLoaded(string libraryName) => throw new NotSupportedException($"Library '{libraryName}' is undefined.");

        public static void ThrowHookCantBeInstalled() => throw new PlatformNotSupportedException();
    }
}
