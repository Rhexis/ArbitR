using System;
using ArbitR.Tester.Read.Queries;
using NUnit.Framework;

namespace ArbitR.Tester.Read
{
    public class Tests : TestBase
    {
        public override void Setup()
        {
            
        }

        [Test]
        public void Should_ThrowError_When_MultipleQueryHandlersRegisteredForOneQuery()
        {
            Exception? exception = null;
            try
            {
                _ = Arbiter.Invoke(new BadQuery());
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