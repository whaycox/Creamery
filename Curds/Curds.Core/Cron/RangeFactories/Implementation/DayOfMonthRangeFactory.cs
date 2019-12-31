namespace Curds.Cron.RangeFactories.Implementation
{
    using RangeLinkFactories.Abstraction;

    internal class DayOfMonthRangeFactory : BaseRangeFactory
    {
        public DayOfMonthRangeFactory(IDayOfMonthRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
