using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Token.Domain
{
    using Abstraction;

    public abstract class Basic : ICronObject
    {
        private List<Range.Domain.Basic> Children = new List<Range.Domain.Basic>();

        public abstract int AbsoluteMin { get; }
        public abstract int AbsoluteMax { get; }

        public Basic(string expressionPart)
        {
            foreach (Range.Domain.Basic range in ParseExpressionPart(expressionPart))
                VerifyAndAddRange(range);
        }
        protected virtual IEnumerable<Range.Domain.Basic> ParseExpressionPart(string expressionPart) => new Parser.Domain.Basic().ParseRanges(expressionPart, this);
        private void VerifyAndAddRange(Range.Domain.Basic childRange)
        {
            if (childRange.Min < AbsoluteMin)
                throw new FormatException($"Cannot have a range less than {AbsoluteMin}");
            if (childRange.Max > AbsoluteMax)
                throw new FormatException($"Cannot have a range greater than {AbsoluteMax}");
            Children.Add(childRange);
        }

        public bool Test(DateTime testTime)
        {
            if (!TestAbsolutes(testTime))
                return false;
            else
                return DoAnyChildrenSucceed(testTime);
        }
        private bool TestAbsolutes(DateTime testTime)
        {
            int datePart = RetrieveDatePart(testTime);
            if (datePart < AbsoluteMin || datePart > AbsoluteMax)
                return false;
            else
                return true;
        }
        protected virtual bool TestChild(DateTime testTime, Range.Domain.Basic childRange) => childRange.Probe(RetrieveDatePart(testTime));
        private bool DoAnyChildrenSucceed(DateTime testTime) => Children.Where(c => TestChild(testTime, c)).Any();

        protected abstract int RetrieveDatePart(DateTime testTime);
    }
}
