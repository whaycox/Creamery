namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using Chains.Abstraction;

    internal class MinuteRangeFactory : BaseRangeFactory, IMinuteRangeFactory
    {
        public MinuteRangeFactory(IMinuteRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
