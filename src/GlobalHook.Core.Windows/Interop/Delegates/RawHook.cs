using System;

namespace GlobalHook.Core.Windows.Interop.Delegates
{
    internal delegate IntPtr RawHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
}
