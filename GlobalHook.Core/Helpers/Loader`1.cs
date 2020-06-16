using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GlobalHook.Core.Helpers
{
    internal static class Loader<T>
    {
        public static IEnumerable<T> Load(Assembly assembly)
        {
            if (assembly.IsDynamic)
                return Enumerable.Empty<T>();

            return assembly
                .ExportedTypes
                .Where(typeof(T).IsAssignableFrom)
                .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsGenericTypeDefinition)
                .Select(x => x.GetConstructor(Type.EmptyTypes))
                .Where(x => x is { })
                .Select(x => (T)x.Invoke(null));
        }

        public static IEnumerable<T> Load(string directory, string searchPattern, EnumerationOptions options)
        {
            if (Directory.Exists(directory))
                return LoadFromDirectory(directory, searchPattern, options);

            throw new DirectoryNotFoundException(directory);
        }

        public static IEnumerable<T> Load(string directory, string searchPattern, SearchOption options) => Load(directory, searchPattern, options == SearchOption.AllDirectories ? new EnumerationOptions { RecurseSubdirectories = true } : new EnumerationOptions());

        public static IEnumerable<T> Load(string directory, string searchPattern) => Load(directory, searchPattern, new EnumerationOptions());

        public static IEnumerable<T> Load(string path)
        {
            if (File.Exists(path))
                return LoadFromFile(path);

            if (Directory.Exists(path))
                return LoadFromDirectory(path, "*.dll", new EnumerationOptions());

            throw new FileNotFoundException(path);
        }

        private static IEnumerable<T> LoadFromFile(string path)
        {
            try
            {
                return Load(Assembly.Load(File.ReadAllBytes(path)));
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

        private static IEnumerable<T> LoadFromDirectory(string path, string searchPattern, EnumerationOptions options) => Directory.EnumerateFiles(path, searchPattern, options).SelectMany(LoadFromFile);
    }
}
