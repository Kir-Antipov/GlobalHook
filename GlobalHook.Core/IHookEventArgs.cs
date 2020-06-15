using System;

namespace GlobalHook.Core
{
    public interface IHookEventArgs
    {
        bool DefaultPrevented { get; }

        void PreventDefault();

        bool CanPreventDefault { get; }

        HookType HookType { get; }

        DateTime Time { get; }
    }
}
