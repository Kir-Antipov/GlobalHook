using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalHook.Core
{
    internal class CombinedHook : IHook
    {
        HookType IHook.HookType => HookType.Undefined;

        public bool CanBeInstalled => true;

        public bool Installed => Array.TrueForAll(Hooks, x => x.Installed);

        public event EventHandler<IHookEventArgs>? OnEvent;

        private readonly IHook[] Hooks;
        private readonly EventHandler<IHookEventArgs> Delegate;

        public CombinedHook(IEnumerable<IHook> hooks)
        {
            Delegate = (sender, args) => OnEvent?.Invoke(sender, args);
            Hooks = hooks.Where(x => x.CanBeInstalled).ToArray();
        }

        public void Install(bool ignoreProcessHasNoWindow = false) => Array.ForEach(Hooks, hook => 
        { 
            hook.OnEvent += Delegate;
            hook.Install(ignoreProcessHasNoWindow); 
        });

        public void Install(long processId, bool ignoreProcessHasNoWindow = false) => Array.ForEach(Hooks, hook =>
        {
            hook.OnEvent += Delegate;
            hook.Install(processId, ignoreProcessHasNoWindow);
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
