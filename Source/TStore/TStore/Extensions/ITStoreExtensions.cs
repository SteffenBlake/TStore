using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TStore.Interfaces;

namespace TStore.Extensions
{
    public static class ITStoreExtensions
    {
        public static void Register(this ITStore store, Type type) => store.Register(type, type);

        public static void Register<TKey, TValue>(this ITStore store)
            where TValue : class, TKey
            => store.Register(typeof(TKey), typeof(TValue));

        public static void Register<T>(this ITStore store) 
            where T: class
            => store.Register<T, T>();

        public static void RegisterNamespace(this ITStore store, string namespacePattern)
        {
            // Create our RegexMatch function
            var regexPattern = namespacePattern.WildCardToRegex();
            bool NamespaceMatch(string nSpace) => Regex.IsMatch(nSpace, regexPattern);

            // Get all public non-abstract classes that match namespace
            var targetClasses = Assembly.GetCallingAssembly().ExportedTypes
                .Where(type => type.IsClass && !type.IsAbstract && NamespaceMatch(type.Namespace))
                .ToArray();

            // Iterate over each class in these namespaces
            foreach (var target in targetClasses)
            {
                // Direct register
                store.Register(target);

                // Also register any inheritted public interfaces on that class to it
                foreach (var interfaceType in target.GetInterfaces().Where(i => i.IsPublic))
                {
                    store.Register(interfaceType, target);
                }
            }
        }

        public static T Fetch<T>(this ITStore store) => (T)store.Fetch(typeof(T));
    }
}
