using System;

namespace Curds.Application.Cron
{
    public interface ICronScheduler
    {
        void Start();
        void Stop();

        void SetOffset(TimeSpan offset);

        void Add(ICronObject cronObject, Action onFire);
        void Clear();
    }
}
