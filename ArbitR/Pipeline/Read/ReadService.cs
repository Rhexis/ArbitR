using ArbitR.Internal.Pipeline.Service;

namespace ArbitR.Pipeline.Read   
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