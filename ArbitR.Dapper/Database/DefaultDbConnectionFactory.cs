using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace ArbitR.Dapper.Database
{
    internal class DefaultDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<DbContext, string> _contexts;

        public DefaultDbConnectionFactory(IDictionary<DbContext, string> contexts)
        {
            _contexts = contexts;
        }
        
        public IDbConnection CreateDbConnection(DbContext context)
        {
            if (_contexts.TryGetValue(context, out var connectionString))
            {
                return new SqlConnection(connectionString);
            }
            throw new InvalidEnumArgumentException($"{nameof(DbContext)}:[{context.ToString()}] not found");
        }
    }
}