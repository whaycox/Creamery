namespace Curds.Cron.RangeFactories.Implementation
{
    using RangeLinkFactories.Abstraction;

    internal class HourRangeFactory : BaseRangeFactory
    {
        public HourRangeFactory(IHourRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
