using ArbitR.Pipeline.Workflows;
using ArbitR.Tester.Workflow.Commands;
using ArbitR.Tester.Workflow.Events;
using ArbitR.Tester.Workflow.Exceptions;
using ArbitR.Tester.Workflow.Queries;
using ArbitR.Tester.Workflow.Results;

namespace ArbitR.Tester.Workflow.Workflows
{
    public class TestWorkflow : Workflow<TestResult>
    {
        private readonly int _id;
        private readonly string _name;
        private readonly string _step2Name;
        
        public TestWorkflow(int id, string name)
        {
            _id = id;
            _name = name;
            _step2Name = _name + " STAGE 2";
            
            AddStep(() => new Step1Command{Id = id, Name = name})
                .OnSuccess(() => new Step1SuccessEvent(_name))
                .OnFailure(() => new Step1FailEvent($"{_name} failed!"));

            AddStep(() => Step2())
                .OnSuccess(() => new Step2SuccessEvent(_step2Name))
                .OnFailure(() => new Step2FailEvent($"{_step2Name} failed!"))
                .OnFailureThrow(e => new TestException(e));
        }

        private Step2Command Step2()
        {
            var result = Arbiter.Invoke(new TestQuery(_id));
            return new Step2Command { Id = result, Name = _step2Name };
        }

        public override TestResult GetResult()
        {
            return new TestResult(_name, _step2Name);
        }
    }
}