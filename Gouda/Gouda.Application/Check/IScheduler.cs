using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain;
using Curds.Application.DateTimes;
using System.Threading.Tasks;

namespace Gouda.Application.Check
{
    using Communication;
    using Persistence;

    public interface IScheduler : IDisposable
    {
        IDateTime Time { get; }
        IPersistence Persistence { get; }
        ISender Sender { get; }

        DateTimeOffset this[int id] { get; }

        void Add(int definitionID);
        void Reschedule(int definitionID, DateTimeOffset rescheduleTime);
        void Remove(int definitionID);

        void Start();
        void Stop();

        void Pause();
        void Resume();
    }
}
