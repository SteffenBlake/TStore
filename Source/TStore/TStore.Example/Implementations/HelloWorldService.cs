using TStore.Example.Interfaces;

namespace TStore.Example.Implementations
{
    public class HelloWorldService : IHelloWorldService
    {
        public string GetHelloWorld() => "Hello World!";
    }
}
