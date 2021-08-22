using ArbitR.Pipeline.Workflows;

namespace ArbitR.Tester
{
    public class TestWorkflow : Workflow<TestCommand, TestResult>
    {
        private string? _stage1;
        private string? _stage2;
        
        public TestWorkflow()
        {
            ForStart()
                .OnSuccess(cmd => new TestSuccessEvent(cmd.Name))
                .OnFailure(cmd => new TestFailEvent($"{cmd.Name} failed!"));

            AddStep(() =>
                {
                    _stage1 = Start.Name;
                    return new Test2Command
                    {
                        Name = Start.Name + " STAGE 2"
                    };
                })
                .OnSuccess(cmd =>
                {
                    _stage2 = cmd.Name;
                    return new TestSuccessEvent(cmd.Name);
                })
                .OnFailure(cmd => new TestFailEvent($"{cmd.Name} failed!"));
        }
        
        public override TestResult GetResult()
        {
            return new TestResult(_stage1!, _stage2!);
        }
    }
}