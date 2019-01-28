using System;

namespace Curds.Application.Cron
{
    public interface IScheduler
    {
        void Start();
        void Stop();

        void SetOffset(TimeSpan offset);

        void Add(ICronObject cronObject, Action onFire);
        void Clear();
    }
}
