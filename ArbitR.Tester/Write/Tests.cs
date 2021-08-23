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
            try
            {
                Arbiter.Invoke(new BadCommand());
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.GetType(), typeof(InvalidOperationException));
                Assert.AreEqual(e.Message, "Sequence contains more than one element");
            }
        }
    }
}