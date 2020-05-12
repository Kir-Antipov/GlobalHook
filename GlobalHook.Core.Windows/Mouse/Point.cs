using GlobalHook.Core.Mouse;

namespace GlobalHook.Core.Windows.Mouse
{
    internal class Point : IPoint
    {
        public int X { get; }
        public int Y { get; }
        public bool IsRelative { get; }

        public Point(int x, int y, bool isRelative = false)
        {
            X = x;
            Y = y;
            IsRelative = isRelative;
        }
    }
}
