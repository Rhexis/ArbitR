namespace ArbitR.Pipeline.ReadModel
{
    public interface IMightHandleEvent<in TEvent> : IHandleEvent<TEvent> where TEvent : IEvent
    {
        bool CanHandle(TEvent eEvent);
    }
}