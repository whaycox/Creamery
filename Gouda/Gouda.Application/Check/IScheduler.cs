using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain;
using Curds.Application.DateTimes;

namespace Gouda.Application.Check
{
    using Communication;
    using Persistence;

    public interface IScheduler
    {
        IDateTime Time { get; set; }
        IPersistence Persistence { get; set; }
        ISender Sender { get; set; }

        void Add(int definitionID);
        void Reschedule(int definitionID, DateTimeOffset rescheduleTime);

        void Start();
        void Stop();

        void Pause();
        void Resume();
    }
}
