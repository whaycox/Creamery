namespace Curds.Cron.RangeLinkFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class DayOfMonthRangeLinkFactory : BaseRangeLinkFactory<DayOfMonthFieldDefinition>, IDayOfMonthRangeLinkFactory
    {
        public DayOfMonthRangeLinkFactory(DayOfMonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
