using System;

namespace Curds.Infrastructure.Cron.Tokens
{
    public class Minute : Basic
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 59;

        public Minute(string expressionPart)
            : base(expressionPart)
        { }

        protected override int RetrieveDatePart(DateTime testTime) => testTime.Minute;
    }
}
