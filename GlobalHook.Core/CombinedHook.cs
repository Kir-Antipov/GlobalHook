using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalHook.Core
{
    internal class CombinedHook : IHook
    {
        public bool CanBeInstalled => true;

        public event EventHandler<IHookEventArgs>? OnEvent;

        private readonly IHook[] Hooks;

        public CombinedHook(IEnumerable<IHook> hooks)
        {
            Hooks = hooks.Where(x => x.CanBeInstalled).ToArray();
            Array.ForEach(Hooks, x => x.OnEvent += (sender, args) => OnEvent?.Invoke(sender, args));
        }

        public void Install() => Array.ForEach(Hooks, x => x.Install());

        public void Install(long threadId) => Array.ForEach(Hooks, x => x.Install(threadId));

        public void Uninstall() => Array.ForEach(Hooks, x => x.Uninstall());

        public void Dispose()
        {
            Uninstall();
            GC.SuppressFinalize(this);
        }

        ~CombinedHook() => Uninstall();
    }
}
