namespace GlobalHook.Core.Mouse
{
    /// <summary>
    /// Represents an ordered pair of integer x- and y-coordinates that defines a point
    /// or a movement vector in a two-dimensional plane.
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// Gets the x-coordinate of <see cref="IPoint"/>.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets the y-coordinate of <see cref="IPoint"/>.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Gets the value that indicates whether the current instance is
        /// a point (<see langword="false"/>) or
        /// a movement vector (<see langword="true"/>).
        /// </summary>
        bool IsRelative { get; }
    }
}
