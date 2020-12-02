using System.Data;
using ArbitR.Core.Event;

namespace ArbitR.Dapper.Handlers
{
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        void Handle(IDbConnection db, TEvent eEvent);
    }
}