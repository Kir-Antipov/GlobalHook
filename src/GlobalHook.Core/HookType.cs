namespace GlobalHook.Core
{
    /// <summary>
    /// Specifies known hook types.
    /// </summary>
    public enum HookType
    {
        /// <summary>
        /// Hook tracks events of several device types.
        /// </summary>
        Mixed = -1,

        /// <summary>
        /// Hook's type is undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Hook tracks keyboard events (e.g., keystrokes).
        /// </summary>
        Keyboard,

        /// <summary>
        /// Hook tracks mouse events (e.g., mouse movement).
        /// </summary>
        Mouse
    }
}
