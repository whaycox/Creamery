using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class Unbounded : Basic
    {
        public Unbounded()
            : base(int.MinValue, int.MaxValue)
        { }

        public override bool Test(Token.Domain.Basic token, DateTime testTime, int testValue) => true;
    }
}
