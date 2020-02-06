﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;

    internal class SqlDatabase : IDatabase
    {
        private ISqlConnectionContext ConnectionContext { get; }

        public SqlDatabase(ISqlConnectionContext connectionContext)
        {
            ConnectionContext = connectionContext;
        }

        public async Task<IDatabaseTransaction> BeginTransaction()
        {
            await ConnectionContext.BeginTransaction();
            return new SqlDatabaseTransaction(ConnectionContext.CommitTransaction, ConnectionContext.RollbackTransaction);
        }
    }
}