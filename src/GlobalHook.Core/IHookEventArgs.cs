using System;

namespace GlobalHook.Core
{
    /// <summary>
    /// Represents the base interface for classes that contain hook event data.
    /// </summary>
    public interface IHookEventArgs
    {
        /// <summary>
        /// Indicates whether default event actions were prevented.
        /// </summary>
        bool DefaultPrevented { get; }

        /// <summary>
        /// Prevents default event actions.
        /// </summary>
        void PreventDefault();

        /// <summary>
        /// Indicates whether default event actions support preventing.
        /// </summary>
        bool CanPreventDefault { get; }

        /// <summary>
        /// Indicates the type of hook event.
        /// </summary>
        HookType HookType { get; }

        /// <summary>
        /// Stores event logging time.
        /// </summary>
        DateTime Time { get; }
    }
}
