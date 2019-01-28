using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class CurdsOptions
    {
        public abstract DateTimes.IProvider Time { get; }
        public abstract Cron.IProvider Cron { get; }
    }
}
