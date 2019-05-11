using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Token.Implementation
{
    using Domain;
    using Enumeration;

    public class Minute : Basic
    {
        public const int MinMinute = 0;
        public const int MaxMinute = 59;

        public override int AbsoluteMin => MinMinute;
        public override int AbsoluteMax => MaxMinute;

        public override Token TokenType => Token.Minute;

        public Minute(IEnumerable<Range.Domain.Basic> ranges)
            : base(ranges)
        { }
    }
}
