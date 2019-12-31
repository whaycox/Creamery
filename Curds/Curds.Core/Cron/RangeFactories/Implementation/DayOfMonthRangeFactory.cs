namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using RangeLinkFactories.Abstraction;

    internal class DayOfMonthRangeFactory : BaseRangeFactory, IDayOfMonthRangeFactory
    {
        public DayOfMonthRangeFactory(IDayOfMonthRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
