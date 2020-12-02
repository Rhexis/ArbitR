using System.Data;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace ArbitR.Dapper.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(DbContext context);
        static TransactionScope CreateTransactionScope() => new TransactionScope
        (
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            }
        );
    }
}