namespace Gouda.Scheduling.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstraction;
    using Gouda.Domain;

    public class Scheduler : IScheduler
    {
        public Task<List<Check>> ChecksBeforeScheduledTime(DateTimeOffset scheduledTime)
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
                    // TODO: dispose managed state (managed objects).
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
