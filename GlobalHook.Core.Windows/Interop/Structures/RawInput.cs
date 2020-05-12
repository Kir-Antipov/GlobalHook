using System.Runtime.InteropServices;

namespace GlobalHook.Core.Windows.Interop.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RawInput
    {
        public RawInputHeader Header;
        public RawInputMouse Mouse;
    }
}
