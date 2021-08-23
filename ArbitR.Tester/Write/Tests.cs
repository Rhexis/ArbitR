using System;
using ArbitR.Tester.Write.Commands;
using NUnit.Framework;

namespace ArbitR.Tester.Write
{
    public class Tests : TestBase
    {
        public override void Setup()
        {
            
        }

        [Test]
        public void Should_ThrowError_When_MultipleCommandHandlersRegisteredForOneCommand()
        {
            Exception? exception = null;
            try
            {
                Arbiter.Invoke(new BadCommand());
            }
            catch (Exception? e)
            {
                exception = e;
            }

            if (exception is null)
            {
                Assert.Fail($"Expected {typeof(InvalidOperationException)} but no exception was thrown!");
            }
            else
            {
                Assert.AreEqual(exception.GetType(), typeof(InvalidOperationException));
                Assert.AreEqual(exception.Message, "Sequence contains more than one element");
            }
        }
    }
}