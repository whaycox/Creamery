using Microsoft.Extensions.DependencyInjection;
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
        private IServiceProvider ServiceProvider { get; }
        private ISqlConnectionStringFactory ConnectionStringFactory { get; }
        private SqlConnectionInformation ConnectionInformation { get; }
        private ISqlQueryReaderFactory QueryReaderFactory { get; }

        private SqlConnection Connection { get; set; }
        private SqlTransaction Transaction { get; set; }

        public SqlConnectionContext(
            IServiceProvider serviceProvider,
            ISqlConnectionStringFactory connectionStringFactory,
            IOptions<SqlConnectionInformation> connectionInformation,
            ISqlQueryReaderFactory queryReaderFactory)
        {
            ServiceProvider = serviceProvider;
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

        public Task BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransaction()
        {
            throw new NotImplementedException();
        }

        private async Task<SqlCommand> BuildCommand(ISqlQuery query)
        {
            ISqlQueryWriter writer = ServiceProvider.GetRequiredService<ISqlQueryWriter>();
            query.Write(writer);

            SqlCommand command = writer.Flush();
            await CheckConnectionIsOpen();
            command.Connection = Connection;
            if (Transaction != null)
                command.Transaction = Transaction;

            return command;
        }

        public async Task Execute(ISqlQuery query)
        {
            SqlCommand command = await BuildCommand(query);
            await command.ExecuteNonQueryAsync();
        }

        public async Task ExecuteWithResult(ISqlQuery query)
        {
            SqlCommand command = await BuildCommand(query);
            using (ISqlQueryReader queryReader = await QueryReaderFactory.Create(command))
                await query.ProcessResult(queryReader);
        }

        public async Task<List<TEntity>> ExecuteWithResult<TEntity>(ISqlQuery<TEntity> query)
            where TEntity : BaseEntity
        {
            SqlCommand command = await BuildCommand(query);
            using (ISqlQueryReader queryReader = await QueryReaderFactory.Create(command))
                await query.ProcessResult(queryReader);
            return query.Results;
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
