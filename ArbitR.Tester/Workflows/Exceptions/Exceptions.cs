using System;

namespace ArbitR.Tester.Workflows.Exceptions
{
    public class TestException : Exception
    {
        private readonly Exception _e;

        public TestException(Exception e)
        {
            _e = e;
        }
    }
}