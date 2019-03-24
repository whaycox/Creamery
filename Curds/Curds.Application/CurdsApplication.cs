using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class CurdsApplication
    {
        protected DateTimes.IDateTime Time { get; }
        protected Cron.ICron Cron { get; }

        public abstract string Description { get; }

        public CurdsApplication(CurdsOptions options)
        {
            Time = options.Time;
            Cron = options.Cron;
        }
    }
}
