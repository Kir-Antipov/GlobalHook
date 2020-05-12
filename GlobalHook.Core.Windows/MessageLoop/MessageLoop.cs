using GlobalHook.Core.MessageLoop;
using GlobalHook.Core.Windows.Interop.Libs;
using GlobalHook.Core.Windows.Interop.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalHook.Core.Windows.MessageLoop
{
    public class MessageLoop : IMessageLoop
    {
        public void Run(IEnumerable<IHook> hooks, Func<bool, bool> goOnPredicate) => Run(hooks, 0, goOnPredicate);

        public void Run(IEnumerable<IHook> hooks, long threadId, Func<bool, bool> goOnPredicate)
        {
            IHook[] applicableHooks = hooks.Where(x => x.CanBeInstalled).ToArray();
            EventHandler<IHookEventArgs> action = InvokeOnEvent;

            for (int i = 0; i < applicableHooks.Length; ++i)
            {
                applicableHooks[i].OnEvent += action;
                applicableHooks[i].Install(threadId);
            }

            bool quit = false;
            while (goOnPredicate(quit))
            {
                while (User32.PeekMessage(out RawMessage msg, IntPtr.Zero, 0, 0, 1))
                {
                    if (msg.Message == 0x0012)
                    {
                        quit = true;
                        break;
                    }

                    User32.TranslateMessage(msg);
                    User32.DispatchMessage(msg);
                }
            }

            for (int i = 0; i < applicableHooks.Length; ++i)
            {
                applicableHooks[i].OnEvent -= action;
                applicableHooks[i].Uninstall();
            }
        }

        private void InvokeOnEvent(object sender, IHookEventArgs e) => OnEvent?.Invoke(sender, e);

        public event EventHandler<IHookEventArgs>? OnEvent;
    }
}
