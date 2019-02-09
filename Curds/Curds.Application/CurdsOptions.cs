using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class CurdsOptions
    {
        public abstract DateTimes.IDateTime Time { get; }
        public abstract Cron.ICron Cron { get; }
    }
}
