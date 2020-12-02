using ArbitR.Core.Event;

namespace ArbitR.Handlers
{
    public interface IMightHandleEvent<in TEvent> : IHandleEvent<TEvent> where TEvent : IEvent
    {
        bool CanHandle(TEvent eEvent);
    }
}