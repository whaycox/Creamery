using System.Collections.Generic;

namespace Curds.Cron.Token.Domain
{
    using Enumeration;

    public class DayOfWeek : Basic
    {
        public const int MinDayOfWeek = 0;
        public const int MaxDayOfWeek = 6;

        public override int AbsoluteMin => MinDayOfWeek;
        public override int AbsoluteMax => MaxDayOfWeek;

        public override ExpressionPart Part => ExpressionPart.DayOfWeek;

        public DayOfWeek(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
