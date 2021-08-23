using System.Reflection;
using ArbitR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ArbitR.Tester
{
    public abstract class TestBase
    {
        protected IArbiter Arbiter = null!;
        
        [SetUp]
        public void Init()
        {
            // DI Container Setup
            var services = new ServiceCollection();
            services.AddArbitR(Assembly.GetExecutingAssembly());
            ServiceProvider provider = services.BuildServiceProvider(false);
            IServiceScope scope = provider.CreateScope();
            Arbiter = scope.ServiceProvider.GetService<IArbiter>()!;
        }

        public abstract void Setup();
    }
}