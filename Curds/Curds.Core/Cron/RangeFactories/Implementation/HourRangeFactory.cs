namespace Curds.Cron.RangeFactories.Implementation
{
    using RangeLinkFactories.Abstraction;
    using Abstraction;

    internal class HourRangeFactory : BaseRangeFactory, IHourRangeFactory
    {
        public HourRangeFactory(IHourRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
