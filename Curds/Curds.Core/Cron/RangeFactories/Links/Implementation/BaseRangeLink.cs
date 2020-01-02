using System;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Abstraction;

    internal abstract class BaseRangeLink<TFieldDefinition> : IRangeFactoryLink
        where TFieldDefinition : ICronFieldDefinition
    {
        protected TFieldDefinition FieldDefinition { get; }

        public IRangeFactoryLink Successor { get; }

        public BaseRangeLink(
            TFieldDefinition fieldDefinition,
            IRangeFactoryLink successor)
        {
            FieldDefinition = fieldDefinition;
            Successor = successor;
        }

        protected bool IsValid(int parsedValue) => FieldDefinition.AbsoluteMin <= parsedValue && parsedValue <= FieldDefinition.AbsoluteMax;

        public abstract ICronRange HandleParse(string range);
    }
}
