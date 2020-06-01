using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Libs
{
    internal static class Kernel32
    {
        public const string LibraryName = "kernel32.dll";

        private static readonly DateTime PCStarted;

        [DllImport(LibraryName, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string libraryName);

        [DllImport(LibraryName)]
        public extern static long GetTickCount64();

        [DllImport(LibraryName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string? moduleName);

        [DllImport(LibraryName)]
        public static extern IntPtr GetConsoleWindow();

        public static DateTime TicksToDateTime(double ticks) => PCStarted.AddMilliseconds(ticks);

        public static double DateTimeToTicks(DateTime time) => (time - PCStarted).TotalMilliseconds;

        static Kernel32() => PCStarted = DateTime.Now.AddMilliseconds(-GetTickCount64());
    }
}
