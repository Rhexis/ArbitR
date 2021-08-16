using ArbitR.Core.Query;
using ArbitR.Handlers;

namespace ArbitR.Services   
{
    /// <summary>
    /// Used for managing a single tables Display actions.
    /// Can consume only Queries.
    /// </summary>
    public abstract class ReadService : ServiceBase, IReadService
    {
        public object? Handle(IQuery query)
        {
            return Invoke(nameof(IHandleQuery<IQuery, object?>.Handle), new object[] {query});
        }
    }
}