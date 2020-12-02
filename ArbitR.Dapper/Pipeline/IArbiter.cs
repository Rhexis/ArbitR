using ArbitR.Core.Command;
using ArbitR.Core.Event;
using ArbitR.Core.Query;

namespace ArbitR.Dapper.Pipeline
{
    public interface IArbiter
    {
        void Invoke(ICommand command);
        T Invoke<T>(IQuery<T> query);
        void Raise(IEvent eEvent);
    }
}