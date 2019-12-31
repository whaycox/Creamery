namespace Curds.Cron.RangeFactories.Implementation
{
    using RangeLinkFactories.Abstraction;

    internal class MonthRangeFactory : BaseRangeFactory
    {
        public MonthRangeFactory(IMonthRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
