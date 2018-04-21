using System;
using TStore.Interfaces;

namespace TStore.Extensions
{
    public static class ITStoreExtensions
    {
        public static void Register(this ITStore store, Type type) => store.Register(type, type);
        public static void Register<TKey, TValue>(this ITStore store) => store.Register(typeof(TKey), typeof(TValue));
        public static void Register<T>(this ITStore store) => store.Register<T, T>();

        public static T Fetch<T>(this ITStore store) => (T)store.Fetch(typeof(T));
    }
}
