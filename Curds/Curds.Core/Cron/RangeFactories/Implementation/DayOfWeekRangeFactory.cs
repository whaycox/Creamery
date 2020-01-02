namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class DayOfWeekRangeFactory : BaseRangeFactory<DayOfWeekFieldDefinition>
    {
        public DayOfWeekRangeFactory(IRangeFactoryChain<DayOfWeekFieldDefinition> rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
