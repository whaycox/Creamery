using System.Collections.Generic;

namespace Curds.Cron.Token.Implementation
{
    using Domain;
    using Enumeration;

    public class Hour : Basic
    {
        public const int MinHour = 0;
        public const int MaxHour = 23;

        public override int AbsoluteMin => MinHour;
        public override int AbsoluteMax => MaxHour;

        public override Token TokenType => Token.Hour;

        public Hour(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
