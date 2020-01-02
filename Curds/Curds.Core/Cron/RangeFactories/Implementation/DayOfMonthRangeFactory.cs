namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class DayOfMonthRangeFactory : BaseRangeFactory<DayOfMonthFieldDefinition>
    {
        public DayOfMonthRangeFactory(IRangeFactoryChain<DayOfMonthFieldDefinition> rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
