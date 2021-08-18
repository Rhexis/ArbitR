namespace ArbitR.Pipeline.ReadModel
{
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent eEvent);
    }
}