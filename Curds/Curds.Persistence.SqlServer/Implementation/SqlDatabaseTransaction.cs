using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;

    internal class SqlDatabaseTransaction : IDatabaseTransaction
    {
        private Func<Task> CommitDelegate { get; }
        private Func<Task> DisposableDelegate { get; }

        public SqlDatabaseTransaction(Func<Task> commitDelegate, Func<Task> disposeDelegate)
        {
            CommitDelegate = commitDelegate;
            DisposableDelegate = disposeDelegate;
        }

        public Task CommitTransaction() => CommitDelegate();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposableDelegate()
                        .GetAwaiter()
                        .GetResult();
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
