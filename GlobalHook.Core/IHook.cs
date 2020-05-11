using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GlobalHook.Core
{
    public interface IHook : IDisposable
    {
        bool CanBeInstalled { get; }

        void Install();

        void Install(long threadId);

        void Uninstall();

        event HookEventHandler<IHookEventArgs>? OnEvent;

        public static IHook Combine(params IHook[] loggers) => new CombinedHook(loggers);

        public static IHook Combine(IEnumerable<IHook> loggers) => new CombinedHook(loggers);

        public static IEnumerable<IHook> Load(Assembly assembly) => assembly
            .ExportedTypes
            .Where(typeof(IHook).IsAssignableFrom).Where(x => !x.IsInterface && !x.IsAbstract)
            .Select(x => x.GetConstructor(Type.EmptyTypes) is { } constructor ? (IHook)constructor.Invoke(null) : null)
            .Where(x => x is { })!;

        public static IEnumerable<IHook> Load(string directory, string searchPattern, EnumerationOptions options)
        {
            if (Directory.Exists(directory))
                return LoadFromDirectory(directory, searchPattern, options);

            throw new DirectoryNotFoundException(directory);
        }

        public static IEnumerable<IHook> Load(string directory, string searchPattern, SearchOption options) => Load(directory, searchPattern, options == SearchOption.AllDirectories ? new EnumerationOptions { RecurseSubdirectories = true } : new EnumerationOptions());

        public static IEnumerable<IHook> Load(string directory, string searchPattern) => Load(directory, searchPattern, new EnumerationOptions());
        
        public static IEnumerable<IHook> Load(string path)
        {
            if (File.Exists(path))
                return LoadFromFile(path);

            if (Directory.Exists(path))
                return LoadFromDirectory(path, "*.dll", new EnumerationOptions());

            throw new FileNotFoundException(path);
        }

        public static IHook LoadCombined(Assembly assembly) => Combine(Load(assembly));

        public static IHook LoadCombined(string path) => Combine(Load(path));

        public static IHook LoadCombined(string directory, string searchPattern) => Combine(Load(directory, searchPattern));

        public static IHook LoadCombined(string directory, string searchPattern, EnumerationOptions options) => Combine(Load(directory, searchPattern, options));

        public static IHook LoadCombined(string directory, string searchPattern, SearchOption options) => Combine(Load(directory, searchPattern, options));

        private static IEnumerable<IHook> LoadFromFile(string path)
        {
            try
            {
                return Load(Assembly.Load(File.ReadAllBytes(path)));
            }
            catch
            {
                return Enumerable.Empty<IHook>();
            }
        }

        private static IEnumerable<IHook> LoadFromDirectory(string path, string searchPattern, EnumerationOptions options) => Directory.EnumerateFiles(path, searchPattern, options).SelectMany(LoadFromFile);
    }
}
