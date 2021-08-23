using ArbitR.Tester.Workflows.Results;
using NUnit.Framework;

namespace ArbitR.Tester.Workflows
{
    public class Tests : TestBase
    {
        public override void Setup()
        {
            
        }

        [Test]
        public void Should_ReturnResult_When_WorkflowSucceeds()
        {
            var testName = "Hello World";
            var workflow = new TestWorkflow(1, testName);
            TestResult result = Arbiter.Begin(workflow);

            Assert.AreEqual(result.Stage1, testName);
            Assert.AreEqual(result.Stage2, testName + " STAGE 2");
        }
    }
}