using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Windows.Interop.Delegates;
using GlobalHook.Core.Windows.Interop.Enums;
using GlobalHook.Core.Windows.Interop.Structures;
using GlobalHook.Core.Windows.Interop.Structures.Raw;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace GlobalHook.Core.Windows.Interop.Libs
{
    internal static class User32
    {
        public const string LibraryName = "user32.dll";

        public const int WheelDelta = 120;

        public static readonly IntPtr Handle;

        public static readonly int DoubleClickTime;

        private const short Pressed = 0b10000000;
        private const short Toggled = 0b00000001;

        [DllImport(LibraryName, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookId idHook, Hook lpfn, IntPtr hMod, int dwThreadId);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName)]
        public static extern int GetMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport(LibraryName)]
        public static extern bool TranslateMessage(in Message lpMsg);

        [DllImport(LibraryName)]
        public static extern IntPtr DispatchMessage(in Message lpMsg);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport(LibraryName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern ushort RegisterClassW(in WindowClasses lpwcx);

        [DllImport(LibraryName, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowExW(int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray)] InputDevice[] pRawInputDevices, int uiNumDevices, int cbSize);

        [DllImport(LibraryName)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName)]
        public static extern int GetRawInputData(IntPtr hRawInput, int uiCommand, out Input pData, ref int pcbSize, int cbSizeHeader);

        [DllImport(LibraryName)]
        public static extern short GetKeyState(Keys key);

        public static bool IsKeyPressed(Keys key) => (GetKeyState(key) & Pressed) == Pressed;

        public static bool IsKeyToggled(Keys key) => (GetKeyState(key) & Toggled) == Toggled;

        [DllImport(LibraryName, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ToAscii(Keys uVirtKey, int uScanCode, byte[] lpKeyState, [Out] StringBuilder lpChar, int uFlags);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport(LibraryName)]
        private static extern int GetDoubleClickTime();

        static User32()
        {
            Handle = Kernel32.LoadLibrary(LibraryName);
            if (Handle == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, $"Failed to load library '{LibraryName}'. Error [{errorCode}]: {new Win32Exception(errorCode).Message}.");
            }

            DoubleClickTime = GetDoubleClickTime();
        }
    }
}
