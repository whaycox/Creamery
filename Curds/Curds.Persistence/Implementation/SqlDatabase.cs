using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;

    internal class SqlDatabase : IDatabase
    {
        private class DisposableTransaction : IDisposable
        {
            private Func<Task> DisposableDelegate { get; }

            public DisposableTransaction(Func<Task> disposableDelegate)
            {
                DisposableDelegate = disposableDelegate;
            }

            public void Dispose() => DisposableDelegate()
                .GetAwaiter()
                .GetResult();
        }

        private ISqlConnectionContext ConnectionContext { get; }

        public SqlDatabase(ISqlConnectionContext connectionContext)
        {
            ConnectionContext = connectionContext;
        }

        public async Task<IDisposable> BeginTransaction()
        {
            await ConnectionContext.BeginTransaction();
            return new DisposableTransaction(ConnectionContext.RollbackTransaction);
        }

        public Task CommitTransaction() => ConnectionContext.CommitTransaction();
    }
}
