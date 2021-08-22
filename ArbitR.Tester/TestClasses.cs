using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Tester
{
    public class Step1Command : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class Step2Command : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class Step1SuccessEvent : IEvent
    {
        public string Message { get; }

        public Step1SuccessEvent(string message)
        {
            Message = message;
        }
    }
    
    public class Step2SuccessEvent : IEvent
    {
        public string Message { get; }

        public Step2SuccessEvent(string message)
        {
            Message = message;
        }
    }
    
    public class Step1FailEvent : IEvent
    {
        public string Message { get; }

        public Step1FailEvent(string message)
        {
            Message = message;
        }
    }
    
    public class Step2FailEvent : IEvent
    {
        public string Message { get; }

        public Step2FailEvent(string message)
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