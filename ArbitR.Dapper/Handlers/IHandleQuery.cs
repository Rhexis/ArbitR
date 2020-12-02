using System.Data;
using ArbitR.Core.Query;

namespace ArbitR.Dapper.Handlers
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery
    {
        TResult Handle(IDbConnection db, TQuery query);
    }
}