using System.Data;
using System.Transactions;
using ArbitR.Core.Query;
using ArbitR.Dapper.Database;
using ArbitR.Dapper.Handlers;

namespace ArbitR.Dapper.Services
{
    public class ReadService : ServiceBase
    {
        protected ReadService() : base(DbContext.ReadModels)
        {
        }

        public object? Handle(IQuery query)
        {
            var success = false;
            using IDbConnection db = DbConnectionFactory.CreateDbConnection(DbContext);
            using TransactionScope scope = IDbConnectionFactory.CreateTransactionScope();
            try
            {
                db.Open();
                object? result = Invoke(nameof(IHandleQuery<IQuery, object?>.Handle), new object[] {db, query});
                scope.Complete();
                success = true;
                return result;
            }
            finally
            {
                scope.Dispose();
                db.Close();
                db.Dispose();
                RaiseQueuedEvents(success);
            }
        }
    }
}