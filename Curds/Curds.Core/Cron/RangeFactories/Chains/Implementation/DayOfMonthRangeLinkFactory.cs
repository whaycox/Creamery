namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using Links;
    using RangeFactories.Abstraction;

    internal class DayOfMonthRangeLinkFactory : BaseRangeLinkFactory<DayOfMonthFieldDefinition>, IDayOfMonthRangeLinkFactory
    {
        public DayOfMonthRangeLinkFactory(DayOfMonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        public override IRangeFactoryLink BuildChain() => base.BuildChain()
            .AddNearestWeekday(FieldDefinition)
            .AddLastDayOfMonth(FieldDefinition);
    }
}
