using System;

namespace Curds.Cron.Range.Implementation
{
    public class Step : Unbounded
    {
        public int StepValueFromMin { get; }

        public Step(int step)
            : base()
        {
            if (step <= 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            StepValueFromMin = step;
        }

        public override bool Test(Token.Domain.Basic token, DateTime testTime) => IsOnStepFromMin(token.AbsoluteMin, token.DatePart(testTime));
        private bool IsOnStepFromMin(int absoluteMin, int testValue) => (testValue - absoluteMin) % StepValueFromMin == 0;

        public override string ToString() => $"{base.ToString()}/{StepValueFromMin}";
    }
}
