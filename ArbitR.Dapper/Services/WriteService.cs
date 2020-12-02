using System.Data;
using System.Transactions;
using ArbitR.Core.Command;
using ArbitR.Dapper.Database;
using ArbitR.Dapper.Handlers;

namespace ArbitR.Dapper.Services
{
    public class WriteService : ServiceBase
    {
        protected WriteService() : base(DbContext.WriteModels)
        {
        }

        public void Handle(ICommand command)
        {
            var success = false;
            using IDbConnection db = DbConnectionFactory.CreateDbConnection(DbContext);
            using TransactionScope scope = IDbConnectionFactory.CreateTransactionScope();
            try
            {
                db.Open();
                Invoke(nameof(IHandleCommand<ICommand>.Handle), new object[] {db, command});
                scope.Complete();
                success = true;
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