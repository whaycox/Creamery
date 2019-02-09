using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class Curds
    {
        protected DateTimes.IDateTime Time { get; }
        protected Cron.ICron Cron { get; }

        public Curds(CurdsOptions options)
        {
            Time = options.Time;
            Cron = options.Cron;
        }
    }
}
