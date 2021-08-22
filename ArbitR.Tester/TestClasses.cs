using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Tester
{
    public class TestCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class Test2Command : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class TestSuccessEvent : IEvent
    {
        public string Message { get; }

        public TestSuccessEvent(string message)
        {
            Message = message;
        }
    }
    
    public class TestFailEvent : IEvent
    {
        public string Message { get; }

        public TestFailEvent(string message)
        {
            Message = message;
        }
    }
    
    public class TestResult
    {
        public string Stage1 { get; }
        public string Stage2 { get; }
        
        public TestResult(string stage1, string stage2)
        {
            Stage1 = stage1;
            Stage2 = stage2;
        }
    }
}