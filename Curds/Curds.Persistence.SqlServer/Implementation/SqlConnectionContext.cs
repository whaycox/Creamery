using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Abstraction;

    internal class SqlConnectionContext : ISqlConnectionContext
    {
        private ILogger Logger { get; }
        private ISqlConnectionStringFactory ConnectionStringFactory { get; }
        private SqlConnectionInformation ConnectionInformation { get; }
        private ISqlQueryReaderFactory QueryReaderFactory { get; }

        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public SqlConnectionContext(
            ILogger<SqlConnectionContext> logger,
            ISqlConnectionStringFactory connectionStringFactory,
            IOptions<SqlConnectionInformation> connectionInformation,
            ISqlQueryReaderFactory queryReaderFactory)
        {
            Logger = logger;
            ConnectionStringFactory = connectionStringFactory;
            ConnectionInformation = connectionInformation.Value;
            QueryReaderFactory = queryReaderFactory;
        }

        private async Task CheckConnectionIsOpen()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionStringFactory.Build(ConnectionInformation));
            if (Connection.State != ConnectionState.Open)
            {
                Logger?.LogInformation("Opening a connection to Sql Server");
                await Connection.OpenAsync();
            }
        }

        public async Task BeginTransaction()
        {
            await CheckConnectionIsOpen();
            Logger?.LogInformation("Beginning a transaction");
            Transaction = Connection.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (Transaction == null)
                throw new InvalidOperationException("Must begin a transaction before rolling it back");
            Logger?.LogInformation("Rolling back a transaction");
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

            Logger?.LogInformation(LogSqlCommand(command));
            return command;
        }
        private string LogSqlCommand(SqlCommand command)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Executing Sql Command:{Environment.NewLine}");
            stringBuilder.AppendLine(command.CommandText);

            if (command.Parameters.Count > 0)
            {
                stringBuilder.AppendLine($"With parameters:{Environment.NewLine}");
                for (int i = 0; i < command.Parameters.Count; i++)
                    stringBuilder.AppendLine($"{command.Parameters[i].ParameterName}: {command.Parameters[i].Value}");
            }

            return stringBuilder.ToString();
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
