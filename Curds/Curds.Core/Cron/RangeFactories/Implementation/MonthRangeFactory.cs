namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using RangeLinkFactories.Abstraction;

    internal class MonthRangeFactory : BaseRangeFactory, IMonthRangeFactory
    {
        public MonthRangeFactory(IMonthRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
