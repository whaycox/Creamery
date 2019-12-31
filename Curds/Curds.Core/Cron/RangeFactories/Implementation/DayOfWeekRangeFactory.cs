namespace Curds.Cron.RangeFactories.Implementation
{
    using RangeLinkFactories.Abstraction;

    internal class DayOfWeekRangeFactory : BaseRangeFactory
    {
        public DayOfWeekRangeFactory(IDayOfWeekRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
