namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Cron.Abstraction;
    using RangeFactories.Abstraction;
    using Links;

    internal abstract class BaseRangeLinkFactory<TFieldDefinition> : IRangeFactoryChain
        where TFieldDefinition : ICronFieldDefinition
    {
        public static IRangeFactoryLink EmptyChain => null;

        protected TFieldDefinition FieldDefinition { get; }

        public BaseRangeLinkFactory(TFieldDefinition fieldDefinition)
        {
            FieldDefinition = fieldDefinition;
        }

        public virtual IRangeFactoryLink BuildChain() => EmptyChain
            .AddWildcard(FieldDefinition)
            .AddRangeValue(FieldDefinition)
            .AddSingleValue(FieldDefinition);
    }
}
