using System.Collections.Generic;

namespace Curds.Cron.Token.Domain
{
    using Enumeration;

    public class Hour : Basic
    {
        public const int MinHour = 0;
        public const int MaxHour = 23;

        public override int AbsoluteMin => MinHour;
        public override int AbsoluteMax => MaxHour;

        public override ExpressionPart Part => ExpressionPart.Hour;

        public Hour(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
