using System;
using System.Collections.Generic;

namespace Curds.Cron.Token.Implementation
{
    using Domain;

    public class Month : Basic
    {
        public override int AbsoluteMin => 1;
        public override int AbsoluteMax => 12;

        public Month(string expressionPart)
            : base(expressionPart)
        { }

        protected override IEnumerable<Range.Domain.Basic> ParseExpressionPart(string expressionPart) => new Parser.Implementation.Month().ParseRanges(expressionPart, this);

        protected override int RetrieveDatePart(DateTime testTime) => testTime.Month;
    }
}
