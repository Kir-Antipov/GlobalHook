using GlobalHook.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GlobalHook.Core
{
    /// <summary>
    /// Represents a hook that can track various events depending on <see cref="IHook.HookType"/> value.
    /// </summary>
    public interface IHook : IDisposable
    {
        /// <summary>
        /// Indicates hook's type.
        /// </summary>
        HookType HookType { get; }

        /// <summary>
        /// Indicates whether the hook is supported by the current environment.
        /// </summary>
        bool CanBeInstalled { get; }

        /// <summary>
        /// Indicates whether the hook supports tracking a specific process.
        /// </summary>
        bool CanBeInstalledIntoProcess { get; }

        /// <summary>
        /// Indicates whether the hook supports preventing default event actions.
        /// </summary>
        bool CanPreventDefault { get; }

        /// <summary>
        /// Indicates whether the hook is currently installed.
        /// </summary>
        bool IsInstalled { get; }

        /// <summary>
        /// Conducts a global hook installation.
        /// </summary>
        /// <inheritdoc cref="Install(long, bool)"/>
        void Install(bool ignoreProcessHasNoWindow = false);

        /// <summary>
        /// Sets the hook to track events of a specific process.
        /// </summary>
        /// <param name="processId">Identifier of the target process.</param>
        /// <param name="ignoreProcessHasNoWindow">
        /// If this parameter is <see langword="false"/>, and the current environment has no built-in message loop (e.g., the console), this method may throw an exception.
        /// </param>
        void Install(long processId, bool ignoreProcessHasNoWindow = false);

        /// <summary>
        /// Uninstalls hook.
        /// </summary>
        void Uninstall();

        /// <summary>
        /// Occurs when the hook catches suitable event.
        /// </summary>
        event EventHandler<IHookEventArgs>? OnEvent;


        /// <summary>
        /// Combines several hooks into one.
        /// </summary>
        /// <param name="hooks">Hooks to be combined.</param>
        /// <returns>Combined hook.</returns>
        public static IHook Combine(IEnumerable<IHook> hooks) => new CombinedHook(hooks);

        /// <inheritdoc cref="Combine(IEnumerable{IHook})"/>
        public static IHook Combine(params IHook[] hooks) => Combine((IEnumerable<IHook>)hooks);


        /// <summary>
        /// Loads all hooks from the given <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly containing hooks.</param>
        /// <returns>Loaded hook instances.</returns>
        public static IEnumerable<IHook> Load(Assembly assembly) => Loader<IHook>.Load(assembly);

        /// <summary>
        /// Loads all hooks from DLLs in the given directory.
        /// </summary>
        /// <param name="directory">Directory containing DLLs.</param>
        /// <param name="searchPattern">
        /// The search string to match against the names of DLLs in path. This parameter
        /// can contain a combination of valid literal path and wildcard (* and ?) characters,
        /// but it doesn't support regular expressions.
        /// </param>
        /// <param name="options">File enumeration options.</param>
        /// <returns>Loaded hook instances.</returns>
        public static IEnumerable<IHook> Load(string directory, string searchPattern, EnumerationOptions options) => Loader<IHook>.Load(directory, searchPattern, options);

        /// <inheritdoc cref="Load(string, string, EnumerationOptions)"/>
        public static IEnumerable<IHook> Load(string directory, string searchPattern, SearchOption options) => Loader<IHook>.Load(directory, searchPattern, options);

        /// <inheritdoc cref="Load(string, string, EnumerationOptions)"/>
        public static IEnumerable<IHook> Load(string directory, string searchPattern) => Loader<IHook>.Load(directory, searchPattern);

        /// <summary>
        /// Loads all hooks from DLLs in the given directory.
        /// <para/>
        /// Loads all hooks from the given DLL filename.
        /// </summary>
        /// <param name="path">Directory containing DLLs or single DLL filename.</param>
        /// <returns>Loaded hook instances.</returns>
        public static IEnumerable<IHook> Load(string path) => Loader<IHook>.Load(path);
    }

    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for managing objects that
    /// implement <see cref="IHook"/>.
    /// </summary>
    public static class HookExtensions
    {
        /// <inheritdoc cref="IHook.Install(long, bool)"/>
        /// <param name="hook">Hook to be installed.</param>
        /// <param name="process">Target process.</param>
        public static void Install(this IHook hook, Process process, bool ignoreProcessHasNoWindow = false) => hook.Install(process.Id, ignoreProcessHasNoWindow);

        /// <inheritdoc cref="IHook.Combine(IEnumerable{IHook})"/>
        public static IHook Combine(this IEnumerable<IHook> hooks) => IHook.Combine(hooks);
    }
}
