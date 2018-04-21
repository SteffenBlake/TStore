using System;
using TStore.Example.Interfaces;
using TStore.Extensions;

namespace TStore.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new TStore.Implementations.TStore();
            store.RegisterNamespace("TStore.Example.Implementations*");

            var consoleService = store.Fetch<IConsoleService>();

            consoleService.PrintHelloWorld();

            Console.ReadKey();
        }
    }
}
