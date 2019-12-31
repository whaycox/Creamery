using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    using Abstraction;

    internal abstract class BaseFieldDefinition : ICronFieldDefinition
    {
        public abstract int AbsoluteMin { get; }
        public abstract int AbsoluteMax { get; }

        public abstract int SelectDatePart(DateTime testTime);
    }
}
