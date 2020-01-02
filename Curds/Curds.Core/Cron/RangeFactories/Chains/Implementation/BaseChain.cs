namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Cron.Abstraction;
    using Links;
    using RangeFactories.Abstraction;

    internal abstract class BaseChain<TFieldDefinition> : IRangeFactoryChain<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public static IRangeFactoryLink EmptyChain => null;

        protected TFieldDefinition FieldDefinition { get; }

        public BaseChain(TFieldDefinition fieldDefinition)
        {
            FieldDefinition = fieldDefinition;
        }

        public virtual IRangeFactoryLink BuildChain() => EmptyChain
            .AddSingleValue(FieldDefinition)
            .AddWildcard(FieldDefinition)
            .AddRangeValue(FieldDefinition);
    }
}
