namespace GlobalHook.Core.Mouse
{
    public interface IPoint
    {
        int X { get; }
        int Y { get; }
        bool IsRelative { get; }
    }
}
