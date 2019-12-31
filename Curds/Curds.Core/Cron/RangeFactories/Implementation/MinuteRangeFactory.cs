namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using RangeLinkFactories.Abstraction;

    internal class MinuteRangeFactory : BaseRangeFactory, IMinuteRangeFactory
    {
        public MinuteRangeFactory(IMinuteRangeLinkFactory rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
