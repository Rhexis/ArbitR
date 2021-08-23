using ArbitR.Pipeline.Workflows;
using ArbitR.Tester.Workflow.Commands;
using ArbitR.Tester.Workflow.Events;
using ArbitR.Tester.Workflow.Exceptions;
using ArbitR.Tester.Workflow.Results;

namespace ArbitR.Tester.Workflow.Workflows
{
    public class TestWorkflow : Workflow<TestResult>
    {
        private Step1Command _step1 = default!;
        private Step2Command _step2 = default!;
        
        public TestWorkflow(int id, string name)
        {
            AddStep(() =>
                {
                    _step1 = new Step1Command{Id = id, Name = name};
                    return _step1;
                })
                .OnSuccess(() => new Step1SuccessEvent(_step1.Name))
                .OnFailure(() => new Step1FailEvent($"{_step1.Name} failed!"));

            AddStep(() =>
                {
                    _step2 = new Step2Command
                    {
                        Id = 2,
                        Name = _step1.Name + " STAGE 2"
                    };
                    return _step2;
                })
                .OnSuccess(() => new Step2SuccessEvent(_step2.Name))
                .OnFailure(() => new Step2FailEvent($"{_step2.Name} failed!"))
                .OnFailureThrow(e => new TestException(e));
        }
        
        public override TestResult GetResult()
        {
            return new TestResult(_step1.Name, _step2.Name);
        }
    }
}