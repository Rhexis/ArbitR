using ArbitR.Core.Event;

namespace ArbitR.Dapper.Services
{
    internal interface IService
    {
        void Handle(IEvent eEvent);
    }
}