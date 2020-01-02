using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    using Abstraction;

    internal abstract class BaseFieldDefinition : ICronFieldDefinition
    {
        public abstract int AbsoluteMin { get; }
        public abstract int AbsoluteMax { get; }

        public virtual int Parse(string value)
        {
            int parsedValue = int.Parse(value);
            if (!IsValid(parsedValue))
                throw new FormatException($"Supplied value {value} is outside the allowed {AbsoluteMin}-{AbsoluteMax}");
            return parsedValue;
        }
        private bool IsValid(int parsedValue) => AbsoluteMin <= parsedValue && parsedValue <= AbsoluteMax;

        public abstract int SelectDatePart(DateTime testTime);
    }
}
