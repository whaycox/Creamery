using System.Collections.Generic;

namespace Curds.Cron.Token.Domain
{
    using Enumeration;

    public class DayOfMonth : Basic
    {
        public const int MinDayOfMonth = 1;
        public const int MaxDayOfMonth = 31;

        public override int AbsoluteMin => MinDayOfMonth;
        public override int AbsoluteMax => MaxDayOfMonth;

        public override ExpressionPart Part => ExpressionPart.DayOfMonth;

        public DayOfMonth(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
