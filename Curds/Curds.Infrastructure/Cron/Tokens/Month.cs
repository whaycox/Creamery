using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Tokens
{
    public class Month : Basic
    {
        public override int AbsoluteMin => 1;
        public override int AbsoluteMax => 12;

        public Month(string expressionPart)
            : base(expressionPart)
        { }

        protected override IEnumerable<Ranges.Basic> ParseExpressionPart(string expressionPart) => Parse(expressionPart, this);

        protected override int RetrieveDatePart(DateTime testTime) => testTime.Month;

        public static IEnumerable<Ranges.Basic> Parse(string expressionPart, Basic token)
        {
            Parsers.Month parser = new Parsers.Month();
            foreach (var range in parser.ParseRanges(expressionPart, token))
                yield return range;
        }
    }
}
