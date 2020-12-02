using ArbitR.Core.Event;

namespace ArbitR.Handlers
{
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent eEvent);
    }
}