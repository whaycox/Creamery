namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using Chains.Abstraction;

    internal class DayOfMonthRangeFactory : BaseRangeFactory, IDayOfMonthRangeFactory
    {
        public DayOfMonthRangeFactory(IDayOfMonthRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
