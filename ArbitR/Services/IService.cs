using ArbitR.Core.Event;

namespace ArbitR.Services
{
    internal interface IService
    {
        void Handle(IEvent eEvent);
    }
}