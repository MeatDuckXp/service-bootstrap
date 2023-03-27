using ServiceBootstrap;

using Test.Sample.Service.Infrastructure;

namespace Test.Sample.Service
{
    internal class Program
    {
        static int Main(string[] args)
        {
            return ServiceBuilder.Create<TestServiceBootstrap>()
                          .StartWithConsoleLifetime(args);
        }
    }
}