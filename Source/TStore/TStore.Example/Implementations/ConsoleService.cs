using System;
using TStore.Example.Interfaces;

namespace TStore.Example.Implementations
{
    public class ConsoleService : IConsoleService
    {
        private IHelloWorldService HelloWorldService { get; }
        public ConsoleService(IHelloWorldService helloWorldService)
        {
            HelloWorldService = helloWorldService;
        }

        public void PrintHelloWorld()
        {
            var message = HelloWorldService.GetHelloWorld();

            Console.WriteLine(message);
        }
    }
}
