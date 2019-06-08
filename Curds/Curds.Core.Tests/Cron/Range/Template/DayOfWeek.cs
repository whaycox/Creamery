using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Range.Template
{
    using Enumeration;

    public abstract class DayOfWeek<T> : Basic<T> where T : Domain.DayOfWeek
    {
        protected abstract DayOfWeek TestDayOfWeek { get; }
        protected int TestArg => (int)TestDayOfWeek;

        protected override ExpressionPart TestingPart => ExpressionPart.DayOfWeek;
    }
}
