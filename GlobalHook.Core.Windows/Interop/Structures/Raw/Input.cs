using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public InputHeader Header;
        public InputMouse Mouse;
    }
}
