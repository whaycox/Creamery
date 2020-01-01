namespace Curds.Cron.RangeLinkFactories.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;

    internal class DayOfMonthRangeLinkFactory : BaseRangeLinkFactory<DayOfMonthFieldDefinition>, IDayOfMonthRangeLinkFactory
    {
        public override ICronRangeLink StartOfChain => base.StartOfChain
            .AddNearestWeekday(FieldDefinition);

        public DayOfMonthRangeLinkFactory(DayOfMonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
