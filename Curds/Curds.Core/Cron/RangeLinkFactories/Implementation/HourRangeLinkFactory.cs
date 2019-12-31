namespace Curds.Cron.RangeLinkFactories.Implementation
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
