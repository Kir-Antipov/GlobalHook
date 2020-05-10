using System;

namespace GlobalHook.Core
{
    public interface IHookEventArgs
    {
        bool DefaultPrevented { get; }

        void PreventDefault();

        bool CanDefaultBePrevented { get; }

        SenderType SenderType { get; }

        DateTime Time { get; }
    }
}
