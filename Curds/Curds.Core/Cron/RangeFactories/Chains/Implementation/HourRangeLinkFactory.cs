namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class HourRangeLinkFactory : BaseRangeLinkFactory<HourFieldDefinition>, IHourRangeLinkFactory
    {
        public HourRangeLinkFactory(HourFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
