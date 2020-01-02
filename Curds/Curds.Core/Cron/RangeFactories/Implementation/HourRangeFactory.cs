namespace Curds.Cron.RangeFactories.Implementation
{
    using Chains.Abstraction;
    using Abstraction;

    internal class HourRangeFactory : BaseRangeFactory, IHourRangeFactory
    {
        public HourRangeFactory(IHourRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
