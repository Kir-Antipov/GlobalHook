using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalHook.Core
{
    internal class CombinedHook : IHook
    {
        public HookType HookType { get; }

        public bool CanBeInstalled => true;

        public bool CanBeInstalledIntoProcess { get; }

        public bool IsInstalled => Array.TrueForAll(Hooks, x => x.IsInstalled);

        public event EventHandler<IHookEventArgs>? OnEvent;

        private readonly IHook[] Hooks;
        private readonly EventHandler<IHookEventArgs> Delegate;

        public CombinedHook(IEnumerable<IHook> hooks)
        {
            Delegate = (sender, args) => OnEvent?.Invoke(sender, args);
            Hooks = hooks.Where(x => x.CanBeInstalled).ToArray();
            CanBeInstalledIntoProcess = Array.TrueForAll(Hooks, x => x.CanBeInstalledIntoProcess);

            HashSet<HookType> hookTypes = new HashSet<HookType>(Array.ConvertAll(Hooks, hook => hook.HookType));
            HookType = hookTypes.Count switch
            {
                0 => HookType.Undefined,
                1 => hookTypes.First(),
                _ => HookType.Mixed
            };
        }

        public void Install(bool ignoreProcessHasNoWindow = false) => Array.ForEach(Hooks, hook => 
        { 
            hook.Install(ignoreProcessHasNoWindow); 
            hook.OnEvent += Delegate;
        });

        public void Install(long processId, bool ignoreProcessHasNoWindow = false) => Array.ForEach(Hooks, hook =>
        {
            hook.Install(processId, ignoreProcessHasNoWindow);
            hook.OnEvent += Delegate;
        });

        public void Uninstall() => Array.ForEach(Hooks, hook => 
        { 
            hook.Uninstall();
            hook.OnEvent -= Delegate;
        });

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Uninstall();
        }

        ~CombinedHook() => Uninstall();
    }
}
