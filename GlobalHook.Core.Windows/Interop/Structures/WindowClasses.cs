using GlobalHook.Core.Windows.Interop.Delegates;
using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WindowClasses
    {
        public uint Style;
        public RawHook Hook;
        public int ClassExtra;
        public int WindowExtra;
        public IntPtr Module;
        public IntPtr Icon;
        public IntPtr Cursor;
        public IntPtr Background;
        public string MenuName;
        public string ClassName;
    }
}
