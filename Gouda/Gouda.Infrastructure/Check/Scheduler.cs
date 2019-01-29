using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.DateTimes;
using Gouda.Application.Check;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Check
{
    public class Scheduler : IScheduler
    {
        public IProvider Time { get; set; }

        public void Start(IEnumerable<Definition> definitions)
        {
            foreach (Definition definition in definitions)
                Add(definition);
        }

        public void Add(Definition definition)
        {
            throw new NotImplementedException();
        }

        public void Pause()
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

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
