using GlobalHook.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GlobalHook.Core
{
    public interface IHook : IDisposable
    {
        HookType HookType { get; }

        bool CanBeInstalled { get; }

        bool CanBeInstalledIntoProcess { get; }

        bool CanPreventDefault { get; }

        bool IsInstalled { get; }

        void Install(bool ignoreProcessHasNoWindow = false);

        void Install(long processId, bool ignoreProcessHasNoWindow = false);

        void Uninstall();

        event EventHandler<IHookEventArgs>? OnEvent;


        public static IHook Combine(IEnumerable<IHook> hooks) => new CombinedHook(hooks);

        public static IHook Combine(params IHook[] hooks) => Combine((IEnumerable<IHook>)hooks);


        public static IEnumerable<IHook> Load(Assembly assembly) => Loader<IHook>.Load(assembly);

        public static IEnumerable<IHook> Load(string directory, string searchPattern, EnumerationOptions options) => Loader<IHook>.Load(directory, searchPattern, options);

        public static IEnumerable<IHook> Load(string directory, string searchPattern, SearchOption options) => Loader<IHook>.Load(directory, searchPattern, options);

        public static IEnumerable<IHook> Load(string directory, string searchPattern) => Loader<IHook>.Load(directory, searchPattern);

        public static IEnumerable<IHook> Load(string path) => Loader<IHook>.Load(path);
    }

    public static class HookExtensions
    {
        public static void Install(this IHook hook, Process process, bool ignoreProcessHasNoWindow = false) => hook.Install(process.Id, ignoreProcessHasNoWindow);

        public static IHook Combine(this IEnumerable<IHook> hooks) => IHook.Combine(hooks);
    }
}
