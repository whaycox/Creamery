using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;
using Gouda.Domain.Communication;

namespace Gouda.Infrastructure.Communication
{
    internal abstract class CronPair<T> where T : ICronEntity
    {
        private IProvider Cron { get; }

        protected T Value { get; private set; }

        public ICronObject Expression { get; private set; }

        public CronPair(IProvider cronProvider, T value)
        {
            Cron = cronProvider;
            Value = value;
            Expression = Cron.Build(Value.CronString);
        }
    }
}
