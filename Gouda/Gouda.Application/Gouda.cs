using System;
using System.Collections.Generic;
using Gouda.Domain;
using Gouda.Domain.Communication;

namespace Gouda.Application
{
    public sealed class Gouda : Curds.Application.Curds, IDisposable
    {
        private Persistence.IPersistence Persistence { get; set; }

        private Check.IExecutor Executor { get; set; }
        private Communication.IListener Listener { get; set; }

        internal Check.IScheduler Scheduler { get; set; }

        public Check.IEvaluator Evaluator { get; set; }
        public Communication.ISender Sender { get; private set; }
        public Communication.INotifier Notifier { get; private set; }

        public Message.Command.Dispatch Commands { get; private set; }

        public Gouda(GoudaOptions options)
            : base(options)
        {
            ReadOptions(options);
            HookupReferences();
            StartServices();
        }
        private void ReadOptions(GoudaOptions options)
        {
            Persistence = options.Persistence;

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
