using System;

namespace TStore.Interfaces
{
    public interface ITStore
    {
        object Fetch(Type type);
        bool IsRegistered(Type type);
        void Register(Type key, Type value);
        void UnRegister(Type type);
    }
}