namespace Curds.Cron.RangeLinkFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class DayOfWeekRangeLinkFactory : BaseRangeLinkFactory<DayOfWeekFieldDefinition>, IDayOfWeekRangeLinkFactory
    {
        public DayOfWeekRangeLinkFactory(DayOfWeekFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
