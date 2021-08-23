using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Tester.Workflows
{
    public class TestWorkflowWriteService : WriteService,
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

    public class TestWorkflowReadModelManager : ReadModelManager,
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