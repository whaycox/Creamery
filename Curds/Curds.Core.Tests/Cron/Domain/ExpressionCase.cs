using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Domain
{
    public abstract class ExpressionCase
    {
        public abstract string Expression { get; }
        public abstract IEnumerable<DateTime> TrueTimes { get; }
        public abstract IEnumerable<DateTime> FalseTimes { get; }
    }
}
