namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MinuteRangeLinkFactory : BaseRangeLinkFactory<MinuteFieldDefinition>, IMinuteRangeLinkFactory
    {
        public MinuteRangeLinkFactory(MinuteFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
