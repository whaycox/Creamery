using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Curds.Application.DateTimes;

namespace Gouda.Application.Check
{
    public interface IScheduler
    {
        IProvider Time { get; set; }

        void Add(Definition definition);
        void Reschedule(int definitionID, DateTimeOffset rescheduleTime);

        void Start(IEnumerable<Definition> definitions);
        void Stop();

        void Pause();
        void Resume();
    }
}
