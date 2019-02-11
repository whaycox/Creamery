using System;
using System.Collections.Generic;
using System.Text;
using Curds.Infrastructure.Cron;

namespace Curds.Domain
{
    public abstract class CronTemplate<T> : TestTemplate<T>
    {
        protected CronProvider Cron = new CronProvider();
    }
}
