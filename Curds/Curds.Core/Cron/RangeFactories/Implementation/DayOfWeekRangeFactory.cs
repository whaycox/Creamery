namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using Chains.Abstraction;

    internal class DayOfWeekRangeFactory : BaseRangeFactory, IDayOfWeekRangeFactory
    {
        public DayOfWeekRangeFactory(IDayOfWeekRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
