using System.Collections.Generic;

namespace Curds.Cron.Token.Implementation
{
    using Domain;
    using Enumeration;

    public class Month : Basic
    {
        public const int MinMonth = 1;
        public const int MaxMonth = 12;

        public override int AbsoluteMin => MinMonth;
        public override int AbsoluteMax => MaxMonth;

        public override Token TokenType => Token.Month;

        public Month(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
