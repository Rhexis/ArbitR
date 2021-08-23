using System.Reflection;
using ArbitR.Pipeline;
using ArbitR.Tester.Workflows;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // DI Container Setup
            var services = new ServiceCollection();
            services.AddArbitR(Assembly.GetExecutingAssembly());
            ServiceProvider provider = services.BuildServiceProvider(false);
            using IServiceScope scope = provider.CreateScope();
            var arbiter = scope.ServiceProvider.GetService<IArbiter>()!;

            var workflow = new TestWorkflow(1, "Hello World");
            TestResult result = arbiter.Begin(workflow);
            
            var a = 5;
        }
    }
}