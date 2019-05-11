using System;

namespace Curds.Cron.Token.Implementation
{
    using Domain;

    public class Hour : Basic
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 23;

        public Hour(string expressionPart)
            : base(expressionPart)
        { }

        protected override int RetrieveDatePart(DateTime testTime) => testTime.Hour;
    }
}
