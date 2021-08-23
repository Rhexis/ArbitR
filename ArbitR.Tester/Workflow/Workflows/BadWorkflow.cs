using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Tester.Workflow.Workflows
{
    public class BadWorkflow : Workflow<string>
    {
        public BadWorkflow()
        {
            Steps.Add(new Step<ICommand>(null!));
        }
        
        public override string GetResult()
        {
            return "";
        }
    }
}