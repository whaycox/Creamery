using Curds.Application.DateTimes;
using Curds.Infrastructure.Collections;
using Gouda.Application.Check;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;
using Gouda.Domain.Check;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Gouda.Infrastructure.Check
{
    public class Scheduler : IScheduler
    {
        private const int DefaultSleepTimeInMs = 750;

        private int SleepTimeInMs { get; }
        private IndexedChronoList Schedule = new IndexedChronoList();
        private CancellationTokenSource CancelSource = new CancellationTokenSource();

        public IDateTime Time { get; set; }
        public IPersistence Persistence { get; set; }
        public ISender Sender { get; set; }

        public DateTimeOffset this[int id] => Schedule[id]?.ScheduledTime ?? throw new KeyNotFoundException($"No definition with the id {id} was found to remove");

        public Scheduler()
            : this(DefaultSleepTimeInMs)
        { }

        public Scheduler(int sleepTime)
        {
            if (sleepTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(sleepTime));
            SleepTimeInMs = sleepTime;
        }

        public void Start()
        {
            foreach (int definitionID in Persistence.Definitions.FetchAll().Select(d => d.ID))
                Schedule.AddNow(definitionID);
            Task.Factory.StartNew(SchedulingThread, CancelSource.Token);
        }

        public void Stop()
        {
            CancelSource.Cancel();
        }

        public void Add(int definitionID) => Schedule.AddNow(definitionID);

        public void Reschedule(int definitionID, DateTimeOffset rescheduleTime)
        {
            var node = Schedule[definitionID];
            if (node == null)
                Schedule.Add(rescheduleTime, definitionID);
            else
            {
                Schedule.Remove(node.Value);
                Schedule.Add(rescheduleTime, node.Value);
            }
        }

        public void Remove(int definitionID) => Schedule.Remove(definitionID);

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        private async void SchedulingThread()
        {
            CancellationToken token = CancelSource.Token;
            while (true)
            {
                foreach (int definitionID in Schedule.Retrieve(Time.Fetch))
                {
                    token.ThrowIfCancellationRequested();
                    Definition definition = Persistence.Definitions.Lookup(definitionID);
                    Sender.Send(definition);
                    Schedule.Add(Time.Fetch.Add(definition.RescheduleSpan), definitionID);
                }

                token.ThrowIfCancellationRequested();
                await Task.Delay(SleepTimeInMs);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    CancelSource.Dispose();
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
