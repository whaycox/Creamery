using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    internal class SqlConnectionContext : ISqlConnectionContext
    {
        private ISqlConnectionStringFactory ConnectionStringFactory { get; }
        private SqlConnectionInformation ConnectionInformation { get; }
        private ISqlQueryReaderFactory QueryReaderFactory { get; }

        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public SqlConnectionContext(
            ISqlConnectionStringFactory connectionStringFactory,
            IOptions<SqlConnectionInformation> connectionInformation,
            ISqlQueryReaderFactory queryReaderFactory)
        {
            ConnectionStringFactory = connectionStringFactory;
            ConnectionInformation = connectionInformation.Value;
            QueryReaderFactory = queryReaderFactory;
        }

        private async Task CheckConnectionIsOpen()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionStringFactory.Build(ConnectionInformation));
            if (Connection.State != ConnectionState.Open)
                await Connection.OpenAsync();
        }

        public async Task BeginTransaction()
        {
            await CheckConnectionIsOpen();
            Transaction = Connection.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (Transaction == null)
                throw new InvalidOperationException("Must begin a transaction before rolling it back");
            Transaction.Rollback();
            Transaction = null;
        }

        public Task CommitTransaction() => Task.Run(() => CommitTransactionInternal());
        private void CommitTransactionInternal()
        {
            if (Transaction == null)
                throw new InvalidOperationException("Must begin a transaction before committing it");
            Transaction.Commit();
            Transaction = null;
        }

        private async Task<SqlCommand> BuildCommand(ISqlQuery query)
        {
            SqlCommand command = query.GenerateCommand();
            await CheckConnectionIsOpen();
            command.Connection = Connection;
            if (Transaction != null)
                command.Transaction = Transaction;

            return command;
        }

        public async Task Execute(ISqlQuery query)
        {
            SqlCommand command = await BuildCommand(query);
            using (ISqlQueryReader queryReader = await QueryReaderFactory.Create(command))
                await query.ProcessResult(queryReader);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Transaction?.Dispose();
                    Connection?.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
