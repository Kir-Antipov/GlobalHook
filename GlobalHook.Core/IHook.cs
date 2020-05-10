using System;

namespace GlobalHook.Core
{
    public interface IHook : IDisposable
    {
        bool CanBeInstalled { get; }

        void Install();

        void Install(long threadId);

        void Uninstall();

        event HookEventHandler<IHookEventArgs>? OnEvent;
    }
}
