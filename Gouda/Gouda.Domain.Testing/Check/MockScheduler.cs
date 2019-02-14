using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.DateTimes;
using Gouda.Application.Check;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;

namespace Gouda.Domain.Check
{
    public class MockScheduler : IScheduler
    {
        public DateTimeOffset this[int id] => throw new NotImplementedException();

        public IDateTime Time { get; set; }
        public IPersistence Persistence { get; set; }
        public ISender Sender { get; set; }

        public void Add(int definitionID)
        {
            throw new NotImplementedException();
        }

        public void Dispose() { }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Remove(int definitionID)
        {
            throw new NotImplementedException();
        }

        public void Reschedule(int definitionID, DateTimeOffset rescheduleTime)
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
