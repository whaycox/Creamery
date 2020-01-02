namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MonthRangeFactory : BaseRangeFactory<MonthFieldDefinition>
    {
        public MonthRangeFactory(IRangeFactoryChain<MonthFieldDefinition> rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
