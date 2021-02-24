using System;
using ArbitR.Core.Pipeline;
using ArbitR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var services = new ServiceCollection();
            services.AddArbitR();
            // Setup DI for the operation providers
            services.AddTransient<TransformOperationProvider>();
            services.AddTransient<ConsumeOperationProvider>();
            // Get the provider
            ServiceProvider provider = services.BuildServiceProvider();
            var arbitr = provider.GetService<IArbiter>();
            
            
            // Test the new pipeline
            var test = new TestPipelineDefinition();
            arbitr!.Execute(test);
            
            
            var a = 5;
        }
    }
}