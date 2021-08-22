using System.Reflection;
using ArbitR.Pipeline;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddArbitR(Assembly.GetExecutingAssembly());
            ServiceProvider provider = services.BuildServiceProvider(false);
            using IServiceScope scope = provider.CreateScope();

            var arbiter = scope.ServiceProvider.GetService<IArbiter>()!;

            var cmd = new TestCommand
            {
                Id = 0,
                Name = "Hello World!"
            };
            
            arbiter.Begin<TestResult>(cmd);

            var a = 5;
        }
    }

    public class TestWriteService : WriteService,
        IHandleCommand<TestCommand>,
        IHandleCommand<Test2Command>
    {
        public void Handle(TestCommand cmd)
        {
            var a = 5;
        }

        public void Handle(Test2Command cmd)
        {
            var a = 5;
        }
    }

    public class TestReadModelManager : ReadModelManager,
        IHandleEvent<TestSuccessEvent>,
        IHandleEvent<TestFailEvent>
    {
        public void Handle(TestSuccessEvent eEvent)
        {
            var a = 5;
        }

        public void Handle(TestFailEvent eEvent)
        {
            var a = 5;
        }
    }
}