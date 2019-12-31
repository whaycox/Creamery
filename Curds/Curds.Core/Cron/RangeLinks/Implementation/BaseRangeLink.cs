using System;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseRangeLink<TFieldDefinition> : ICronRangeLink
        where TFieldDefinition : ICronFieldDefinition
    {
        protected TFieldDefinition FieldDefinition { get; }

        public ICronRangeLink Successor { get; }

        public BaseRangeLink(
            TFieldDefinition fieldDefinition,
            ICronRangeLink successor)
        {
            FieldDefinition = fieldDefinition;
            Successor = successor;
        }

        public abstract ICronRange HandleParse(string range);
    }
}
