using System;
using Curds.Application;

namespace Gouda.Application
{
    public sealed class GoudaApplication : CurdsApplication, IDisposable
    {
        internal Persistence.IPersistence Persistence { get; set; }

        internal Security.ISecurity Security { get; set; }

        internal Check.IExecutor Executor { get; set; }
        internal Check.IEvaluator Evaluator { get; set; }
        internal Check.IScheduler Scheduler { get; set; }

        internal Communication.IListener Listener { get; set; }
        internal Communication.ISender Sender { get; set; }
        internal Communication.INotifier Notifier { get; set; }

        public Message.Command.Dispatch Commands { get; private set; }

        public GoudaApplication(GoudaOptions options)
            : base(options)
        {
            ReadOptions(options);
            HookupReferences();
            StartServices();
        }
        private void ReadOptions(GoudaOptions options)
        {
            Persistence = options.Persistence;

            Security = options.Security;

            Executor = options.Executor;
            Scheduler = options.Scheduler;
            Listener = options.Listener;

            Evaluator = options.Evaluator;
            Sender = options.Sender;
            Notifier = options.Notifier;
        }
        private void HookupReferences()
        {
            Listener.Handler = Executor.Perform;
            Notifier.Persistence = Persistence;
            Notifier.Time = Time;
            Scheduler.Time = Time;
        }
        private void StartServices()
        {
            Listener.Start();
            Commands = new Message.Command.Dispatch(this);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Scheduler.Dispose();
                    Listener.Dispose();
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
