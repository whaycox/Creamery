using System;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Abstraction;

    internal abstract class BaseLink<TFieldDefinition> : IRangeFactoryLink
        where TFieldDefinition : ICronFieldDefinition
    {
        protected TFieldDefinition FieldDefinition { get; }

        public IRangeFactoryLink Successor { get; }

        public BaseLink(
            TFieldDefinition fieldDefinition,
            IRangeFactoryLink successor)
        {
            FieldDefinition = fieldDefinition;
            Successor = successor;
        }


        public abstract ICronRange HandleParse(string range);
    }
}
