using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;

    internal class SqlDatabaseTransaction : IDatabaseTransaction
    {
        public ISqlConnectionContext ConnectionContext { get; }

        public SqlDatabaseTransaction(ISqlConnectionContext connectionContext)
        {
            ConnectionContext = connectionContext;
        }

        public Task CommitTransaction() => ConnectionContext.CommitTransaction();
        public Task RollbackTransaction() => Task.Run(() => ConnectionContext.RollbackTransaction());

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    ConnectionContext?.RollbackTransaction();
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
