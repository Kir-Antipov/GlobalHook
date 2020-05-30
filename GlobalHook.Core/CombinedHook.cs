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
        private readonly EventHandler<IHookEventArgs> Delegate;

        public CombinedHook(IEnumerable<IHook> hooks)
        {
            Delegate = (sender, args) => OnEvent?.Invoke(sender, args);
            Hooks = hooks.Where(x => x.CanBeInstalled).ToArray();
        }

        public void Install() => Array.ForEach(Hooks, hook => 
        { 
            hook.OnEvent += Delegate;
            hook.Install(); 
        });

        public void Install(long processId) => Array.ForEach(Hooks, hook =>
        {
            hook.OnEvent += Delegate;
            hook.Install(processId);
        });

        public void Uninstall() => Array.ForEach(Hooks, hook => 
        { 
            hook.OnEvent -= Delegate;
            hook.Uninstall();
        });

        public void Dispose()
        {
            Uninstall();
            GC.SuppressFinalize(this);
        }

        ~CombinedHook() => Uninstall();
    }
}
