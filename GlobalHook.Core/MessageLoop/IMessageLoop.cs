using System;
using System.Collections.Generic;
using System.Threading;

namespace GlobalHook.Core.MessageLoop
{
    public interface IMessageLoop
    {
        /// <summary>
        /// The message loop must be started in the main thread.
        /// It locks the thread, automatically installs hooks (<see cref="IHook.Install"/>) at startup 
        /// and uninstalls them (<see cref="IHook.Uninstall"/>) after it's finished.
        /// </summary>
        /// <param name="hooks">Hooks to be launched.</param>
        /// <param name="threadId">The identifier of the target thread.</param>
        /// <param name="goOnPredicate">Callback that indicates whether to continue (<see langword="true"/>) the message loop.</param>
        void Run(IEnumerable<IHook> hooks, long threadId, Func<bool> goOnPredicate);

        /// <inheritdoc cref="Run(IEnumerable{IHook}, long, Func{bool})"/>
        void Run(IEnumerable<IHook> hooks, Func<bool> goOnPredicate);

        /// <summary>
        /// This is an aggregated event for all running hooks.
        /// </summary>
        event HookEventHandler<IHookEventArgs>? OnEvent;
    }

    public static class MessageLoopExtensions
    {
        /// <inheritdoc cref="IMessageLoop.Run(IEnumerable{IHook}, Func{bool})"/>
        /// <param name="loop">Message loop instance.</param>
        public static void Run(this IMessageLoop loop, long threadId, Func<bool> goOnPredicate, params IHook[] hooks) => loop.Run(hooks, threadId, goOnPredicate);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        public static void Run(this IMessageLoop loop, Func<bool> goOnPredicate, params IHook[] hooks) => loop.Run(hooks, goOnPredicate);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        /// <param name="cancellationToken">The message <paramref name="loop"/> will be stopped immediately after the cancel request for this token.</param>
        public static void Run(this IMessageLoop loop, IEnumerable<IHook> hooks, long threadId, CancellationToken cancellationToken) => loop.Run(hooks, threadId, () => !cancellationToken.IsCancellationRequested);
        
        /// <inheritdoc cref="Run(IMessageLoop, IEnumerable{IHook}, long, CancellationToken)"/>
        public static void Run(this IMessageLoop loop, IEnumerable<IHook> hooks, CancellationToken cancellationToken) => loop.Run(hooks, () => !cancellationToken.IsCancellationRequested);

        /// <inheritdoc cref="Run(IMessageLoop, IEnumerable{IHook}, long, CancellationToken)"/>
        public static void Run(this IMessageLoop loop, long threadId, CancellationToken cancellationToken, params IHook[] hooks) => loop.Run(hooks, threadId, () => !cancellationToken.IsCancellationRequested);
        
        /// <inheritdoc cref="Run(IMessageLoop, IEnumerable{IHook}, long, CancellationToken)"/>
        public static void Run(this IMessageLoop loop, CancellationToken cancellationToken, params IHook[] hooks) => loop.Run(hooks, () => !cancellationToken.IsCancellationRequested);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        public static void Run(this IMessageLoop loop, long threadId, params IHook[] hooks) => loop.Run(hooks, threadId, () => true);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        public static void Run(this IMessageLoop loop, IEnumerable<IHook> hooks, long threadId) => loop.Run(hooks, threadId, () => true);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        public static void Run(this IMessageLoop loop, params IHook[] hooks) => loop.Run(hooks, () => true);

        /// <inheritdoc cref="Run(IMessageLoop, long, Func{bool}, IHook[])"/>
        public static void Run(this IMessageLoop loop, IEnumerable<IHook> hooks) => loop.Run(hooks, () => true);
    }
}
