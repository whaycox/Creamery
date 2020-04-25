using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;

    internal class SqlQueryReaderFactory : ISqlQueryReaderFactory
    {
        public async Task<ISqlQueryReader> Create(SqlCommand command) => new SqlQueryReader(await command.ExecuteReaderAsync());
    }
}
