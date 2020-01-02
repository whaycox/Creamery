namespace Curds.Cron.RangeFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MinuteRangeFactory : BaseRangeFactory<MinuteFieldDefinition>
    {
        public MinuteRangeFactory(IRangeFactoryChain<MinuteFieldDefinition> rangeLinkFactory)
            : base(rangeLinkFactory)
        { }
    }
}
