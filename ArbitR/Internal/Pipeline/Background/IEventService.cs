using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Internal.Pipeline.Background
{
    internal interface IEventService
    {
        void Handle(IEvent eEvent);
    }
}