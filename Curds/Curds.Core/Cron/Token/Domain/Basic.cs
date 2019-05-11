using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Token.Domain
{
    using Abstraction;
    using Enumeration;

    public abstract class Basic : ICronObject
    {
        private List<Range.Domain.Basic> Children = new List<Range.Domain.Basic>();

        public abstract int AbsoluteMin { get; }
        public abstract int AbsoluteMax { get; }

        public abstract Token TokenType { get; }

        public Basic(IEnumerable<Range.Domain.Basic> ranges)
        {
            if (ranges == null || ranges.Count() == 0)
                throw new ArgumentNullException(nameof(ranges));
            Children.AddRange(ranges);
        }

        public bool Test(DateTime testTime) =>  DoAnyChildrenSucceed(testTime);
        private bool DoAnyChildrenSucceed(DateTime testTime) => Children.Where(c => TestChild(testTime, c)).Any();
        protected virtual bool TestChild(DateTime testTime, Range.Domain.Basic childRange) => childRange.Test(this, testTime, RetrieveDatePart(testTime));
        private int RetrieveDatePart(DateTime testTime)
        {
            switch (TokenType)
            {
                case Token.Minute:
                    return testTime.Minute;
                case Token.Hour:
                    return testTime.Hour;
                case Token.DayOfMonth:
                    return testTime.Day;
                case Token.Month:
                    return testTime.Month;
                case Token.DayOfWeek:
                    return (int)testTime.DayOfWeek;
                default:
                    throw new InvalidOperationException($"Unexpected {nameof(TokenType)}: {TokenType}");
            }
        }
    }
}
