using ArbitR.Core.Query;
using ArbitR.Handlers;

namespace ArbitR.Services   
{
    public abstract class ReadService : ServiceBase, IReadService
    {
        public object? Handle(IQuery query)
        {
            return Invoke(nameof(IHandleQuery<IQuery, object?>.Handle), new object[] {query});
        }
    }
}