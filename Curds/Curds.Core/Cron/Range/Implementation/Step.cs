using System;

namespace Curds.Cron.Range.Implementation
{
    using Domain;

    internal class Step : Basic
    {
        private int StepValueFromMin { get; }

        public Step(int min, int max, int step)
            : base(min, max)
        {
            if (step <= 0)
                throw new ArgumentOutOfRangeException(nameof(step));
            StepValueFromMin = step;
        }

        public override bool Probe(int testValue) => base.Probe(testValue) && IsOnStepFromMin(testValue);
        private bool IsOnStepFromMin(int testValue) => (testValue - Min) % StepValueFromMin == 0;
    }
}
