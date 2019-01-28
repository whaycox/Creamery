using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron
{
    public abstract class CronCase<T> where T : ICronObject
    {
        public abstract IEnumerable<T> Samples { get; }
        public abstract IEnumerable<DateTime> TrueTimes { get; }
        public abstract IEnumerable<DateTime> FalseTimes { get; }
    }
}
