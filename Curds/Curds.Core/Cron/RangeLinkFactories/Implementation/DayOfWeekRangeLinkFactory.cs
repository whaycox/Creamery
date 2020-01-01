namespace Curds.Cron.RangeLinkFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;

    internal class DayOfWeekRangeLinkFactory : BaseRangeLinkFactory<DayOfWeekFieldDefinition>, IDayOfWeekRangeLinkFactory
    {
        public override ICronRangeLink StartOfChain => base.StartOfChain
            .AddNthDayOfWeek(FieldDefinition)
            .AddLastDayOfWeek(FieldDefinition);

        public DayOfWeekRangeLinkFactory(DayOfWeekFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
