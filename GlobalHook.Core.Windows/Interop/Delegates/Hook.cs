using System;

namespace GlobalHook.Core.Windows.Interop.Delegates
{
    internal delegate IntPtr Hook(int nCode, IntPtr wParam, IntPtr lParam);
}
