namespace Curds.Cron.RangeLinkFactories.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseRangeLinkFactory<TFieldDefinition> : ICronRangeLinkFactory
        where TFieldDefinition : ICronFieldDefinition
    {
        public static ICronRangeLink EmptyChain => null;

        protected TFieldDefinition FieldDefinition { get; }

        public virtual ICronRangeLink StartOfChain => EmptyChain
            .AddWildcard()
            .AddSingleValue(FieldDefinition);

        public BaseRangeLinkFactory(TFieldDefinition fieldDefinition)
        {
            FieldDefinition = fieldDefinition;
        }
    }
}
