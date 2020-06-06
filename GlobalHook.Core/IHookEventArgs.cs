using System;

namespace GlobalHook.Core
{
    public interface IHookEventArgs
    {
        bool DefaultPrevented { get; }

        void PreventDefault();

        bool CanDefaultBePrevented { get; }

        HookType HookType { get; }

        DateTime Time { get; }
    }
}
