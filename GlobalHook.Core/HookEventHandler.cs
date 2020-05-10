using System;

namespace GlobalHook.Core
{
    public delegate void HookEventHandler(object sender, EventArgs e);

    public delegate void HookEventHandler<in TEventArgs>(object sender, TEventArgs e) where TEventArgs : IHookEventArgs;

    public delegate void HookEventHandler<in THook, in TEventArgs>(THook sender, TEventArgs e) where THook : IHook where TEventArgs : IHookEventArgs;
}
