using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class Curds
    {
        protected DateTimes.IProvider Time { get; }
        protected Cron.IProvider Cron { get; }

        public Curds(CurdsOptions options)
        {
            Time = options.Time;
            Cron = options.Cron;
        }
    }
}
