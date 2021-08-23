using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Tester.Workflows.Events
{
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
}