using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Cron.Range.Mock
{
    public class Basic : Domain.Basic
    {
        public bool Success { get; set; } = true;

        public Basic()
            : base(int.MinValue, int.MaxValue)
        { }

        public List<int> SuppliedDateParts = new List<int>();
        public override bool Test(Token.Domain.Basic token, DateTime testTime, int testValue)
        {
            SuppliedDateParts.Add(testValue);
            return Success;
        }
    }
}
