using System;
using System.Collections.Generic;

namespace Curds.Cron.Range.Mock
{
    public class Basic : Domain.Basic
    {
        private bool TestValidity = true;

        public bool Success { get; set; } = true;

        public Basic(int min, int max)
            : base(min, max)
        { }

        public Basic(bool success)
            : base(int.MinValue, int.MaxValue)
        {
            TestValidity = false;
            Success = success;
        }

        public override bool IsValid(int absoluteMin, int absoluteMax)
        {
            if (TestValidity)
                return base.IsValid(absoluteMin, absoluteMax);
            else
                return true;
        }

        public List<DateTime> SuppliedTimes = new List<DateTime>();
        public override bool Test(Token.Domain.Basic token, DateTime testTime)
        {
            SuppliedTimes.Add(testTime);
            return Success;
        }
    }
}
