namespace GlobalHook.Core.Mouse
{
    /// <summary>
    /// Specifies known mouse actions.
    /// </summary>
    public enum MouseEventType
    {
        /// <summary>
        /// Mouse event type is undefined.
        /// </summary>
        None,

        /// <summary>
        /// Mouse button was pressed.
        /// </summary>
        Key,

        /// <summary>
        /// Mouse wheel was rotated.
        /// </summary>
        Wheel,

        /// <summary>
        /// Mouse horizontal wheel was rotated.
        /// </summary>
        HorizontalWheel,

        /// <summary>
        /// Mouse was moved.
        /// </summary>
        Movement
    }
}
