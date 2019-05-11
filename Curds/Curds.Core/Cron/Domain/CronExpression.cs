using System;

namespace Curds.Cron.Domain
{
    using Abstraction;

    public abstract class CronExpression : ICronObject
    {
        public abstract string Expression { get; }
        public abstract bool Test(DateTime testTime);
    }
}
