namespace Curds.Cron.RangeFactories.Implementation
{
    using FieldDefinitions.Implementation;
    using Abstraction;

    internal class HourRangeFactory : BaseRangeFactory<HourFieldDefinition>
    {
        public HourRangeFactory(IRangeFactoryChain<HourFieldDefinition> rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
