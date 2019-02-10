using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;
using Gouda.Application.Communication;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseListener : Communicator, IListener
    {
        public RequestHandler Handler { get; set; }

        public bool IsStarted { get; private set; }
        
        public virtual void Start()
        {
            if (IsStarted)
                throw new InvalidOperationException($"Cannot start a {nameof(BaseListener)} that is already started");
            IsStarted = true;
        }
        public virtual void Stop()
        {
            if (!IsStarted)
                throw new InvalidOperationException($"Cannot stop a {nameof(BaseListener)} that isn't started");
            IsStarted = false;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (IsStarted)
                    Stop();
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
