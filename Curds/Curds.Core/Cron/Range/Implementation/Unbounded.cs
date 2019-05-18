using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class Unbounded : Basic
    {
        public const char Wildcard = '*';

        public Unbounded()
            : base(int.MinValue, int.MaxValue)
        { }

        public override bool IsValid(int absoluteMin, int absoluteMax) => true;
        public override bool Test(Token.Domain.Basic token, DateTime testTime) => true;

        public override string ToString() => Wildcard.ToString();
    }
}
