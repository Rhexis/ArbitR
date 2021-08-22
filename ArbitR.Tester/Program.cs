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

            var workflow = new TestWorkflow(1, "Hello World");
            
            TestResult result = arbiter.Begin(workflow);

            var a = 5;
        }
    }

    public class TestWriteService : WriteService,
        IHandleCommand<Step1Command>,
        IHandleCommand<Step2Command>
    {
        public void Handle(Step1Command cmd)
        {
            var a = 5;
        }

        public void Handle(Step2Command cmd)
        {
            var a = 5;
        }
    }

    public class TestReadModelManager : ReadModelManager,
        IHandleEvent<Step1SuccessEvent>,
        IHandleEvent<Step1FailEvent>,
        IHandleEvent<Step2SuccessEvent>,
        IHandleEvent<Step2FailEvent>
    {
        public void Handle(Step1SuccessEvent eEvent)
        {
            var a = 5;
        }

        public void Handle(Step1FailEvent eEvent)
        {
            var a = 5;
        }

        public void Handle(Step2SuccessEvent eEvent)
        {
            var a = 5;
        }

        public void Handle(Step2FailEvent eEvent)
        {
            var a = 5;
        }
    }
}