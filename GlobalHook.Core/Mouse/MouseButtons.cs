using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Core.Mouse
{
    /// <summary>
    /// Specifies constants that define which mouse button was pressed.
    /// </summary>
    [Flags]
    [ComVisible(true)]
    public enum MouseButtons
    {
        /// <summary>
        /// No mouse button was pressed.
        /// </summary>
        None = 0,

        /// <summary>
        /// The left mouse button was pressed.
        /// </summary>
        Left = 1048576,

        /// <summary>
        /// The right mouse button was pressed.
        /// </summary>
        Right = 2097152,

        /// <summary>
        /// The middle mouse button was pressed.
        /// </summary>
        Middle = 4194304,

        /// <summary>
        /// The middle mouse button was pressed.
        /// </summary>
        Wheel = 4194304,

        /// <summary>
        /// The first XButton (XBUTTON1) on Microsoft IntelliMouse Explorer was pressed.
        /// </summary>
        XButton1 = 8388608,

        /// <summary>
        /// The second XButton (XBUTTON2) on Microsoft IntelliMouse Explorer was pressed.
        /// </summary>
        XButton2 = 16777216
    }
}
