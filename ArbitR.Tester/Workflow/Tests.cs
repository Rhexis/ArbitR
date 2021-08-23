using System;
using ArbitR.Tester.Workflow.Results;
using ArbitR.Tester.Workflow.Workflows;
using NUnit.Framework;

namespace ArbitR.Tester.Workflow
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

        [Test]
        public void Should_ThrowError_When_NullCommandInWorkflow()
        {
            Exception? exception = null;
            var workflow = new BadWorkflow();
            
            try
            {
                Arbiter.Begin(workflow);
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception is null)
            {
                Assert.Fail($"Expected {typeof(NullReferenceException)} but no exception was thrown!");
            }
            else
            {
                Assert.AreEqual(exception.GetType(), typeof(NullReferenceException));
                Assert.AreEqual(exception.Message, $"Misconfigured step in workflow[{workflow.GetType()}], step had no command");
            }
        }
    }
}