using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Curds.Persistence.Domain;
    using Domain;
    using System.Collections.Generic;

    internal class SqlConnectionContext : ISqlConnectionContext
    {
        private ISqlConnectionStringFactory ConnectionStringFactory { get; }
        private SqlConnectionInformation ConnectionInformation { get; }
        private ISqlQueryWriterFactory QueryWriterFactory { get; }

        private SqlConnection Connection { get; set; }
        private SqlTransaction Transaction { get; set; }

        public SqlConnectionContext(
            ISqlConnectionStringFactory connectionStringFactory,
            IOptions<SqlConnectionInformation> connectionInformation,
            ISqlQueryWriterFactory queryWriterFactory)
        {
            ConnectionStringFactory = connectionStringFactory;
            ConnectionInformation = connectionInformation.Value;
            QueryWriterFactory = queryWriterFactory;
        }

        private Task CheckConnectionIsOpen()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConnectionStringFactory.Build(ConnectionInformation));
            return Connection.OpenAsync();
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

        public async Task Execute(ISqlQuery query)
        {
            ISqlQueryWriter writer = QueryWriterFactory.Create();
            query.Write(writer);

            SqlCommand command = writer.Flush();
            await CheckConnectionIsOpen();
            command.Connection = Connection;
            if (Transaction != null)
                command.Transaction = Transaction;

            await command.ExecuteNonQueryAsync();
        }

        public Task<List<TEntity>> Execute<TEntity>(ISqlQuery<TEntity> query) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
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
