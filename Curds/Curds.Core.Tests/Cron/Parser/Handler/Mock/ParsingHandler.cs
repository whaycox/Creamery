using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Mock
{
    using Range.Domain;

    public class ParsingHandler : Domain.ParsingHandler
    {
        public ParsingHandler()
            : base(null)
        { }

        public List<string> RangesHandled = new List<string>();
        protected override Basic HandleParseInternal(string range)
        {
            RangesHandled.Add(range);
            return new Range.Mock.Basic(true);
        }
    }
}
