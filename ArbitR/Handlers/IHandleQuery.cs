using ArbitR.Core.Query;

namespace ArbitR.Handlers
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}