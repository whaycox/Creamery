using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    public class Step : Basic
    {
        private int StepValueFromMin { get; }

        public Step(int step)
            : base(int.MinValue, int.MaxValue)
        {
            if (step <= 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            StepValueFromMin = step;
        }

        public override bool Test(Token.Domain.Basic token, DateTime testTime, int testValue) =>
            base.Test(token, testTime, testValue) && IsOnStepFromMin(token.AbsoluteMin, testValue);
        private bool IsOnStepFromMin(int absoluteMin, int testValue) => (testValue - absoluteMin) % StepValueFromMin == 0;
    }
}
