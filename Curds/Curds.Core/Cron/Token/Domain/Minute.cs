using System.Collections.Generic;

namespace Curds.Cron.Token.Domain
{
    using Enumeration;

    public class Minute : Basic
    {
        public const int MinMinute = 0;
        public const int MaxMinute = 59;

        public override int AbsoluteMin => MinMinute;
        public override int AbsoluteMax => MaxMinute;

        public override ExpressionPart Part => ExpressionPart.Minute;

        public Minute(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
