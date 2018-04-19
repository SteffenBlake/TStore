using System;
using TStore.Example.Implementations;
using TStore.Example.Interfaces;

namespace TStore.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new TStore.Implementations.TStore();
            store.Register(typeof(IConsoleService), typeof(ConsoleService));
            store.Register(typeof(IHelloWorldService), typeof(HelloWorldService));

            var consoleService = (IConsoleService) store.Fetch(typeof(IConsoleService));

            consoleService.PrintHelloWorld();

            Console.ReadKey();
        }
    }
}
