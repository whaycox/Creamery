using System;

namespace Curds.Cron.Ranges.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseRange<TFieldDefinition> : ICronRange<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        protected TFieldDefinition FieldDefinition { get; }

        public BaseRange(TFieldDefinition fieldDefinition)
        {
            FieldDefinition = fieldDefinition;
        }

        public abstract bool IsActive(DateTime testTime);
    }
}
