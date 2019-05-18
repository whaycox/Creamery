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

        public abstract ExpressionPart Part { get; }

        public Basic(IEnumerable<Range.Domain.Basic> ranges)
        {
            if (ranges == null || ranges.Count() == 0)
                throw new ArgumentNullException(nameof(ranges));
            foreach (var range in ranges)
            {
                ValidateRange(range);
                Children.Add(range);
            }
        }
        private void ValidateRange(Range.Domain.Basic childRange)
        {
            if (!childRange.IsValid(AbsoluteMin, AbsoluteMax))
                throw new ArgumentOutOfRangeException($"{childRange} is outside the bounds {AbsoluteMin}-{AbsoluteMax}");
        }

        public bool Test(DateTime testTime) => DoAnyChildrenSucceed(testTime);
        private bool DoAnyChildrenSucceed(DateTime testTime) => Children.Where(c => TestChild(testTime, c)).Any();
        private bool TestChild(DateTime testTime, Range.Domain.Basic childRange) => childRange.Test(this, testTime);

        public int DatePart(DateTime testTime)
        {
            switch (Part)
            {
                case ExpressionPart.Minute:
                    return testTime.Minute;
                case ExpressionPart.Hour:
                    return testTime.Hour;
                case ExpressionPart.DayOfMonth:
                    return testTime.Day;
                case ExpressionPart.Month:
                    return testTime.Month;
                case ExpressionPart.DayOfWeek:
                    return (int)testTime.DayOfWeek;
                default:
                    throw new InvalidOperationException($"Unexpected {nameof(Part)}: {Part}");
            }
        }
    }
}
