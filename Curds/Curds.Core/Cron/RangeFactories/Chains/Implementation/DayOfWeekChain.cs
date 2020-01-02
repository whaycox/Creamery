namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;
    using Cron.Abstraction;
    using RangeFactories.Abstraction;
    using Links;

    internal class DayOfWeekChain : BaseChain<DayOfWeekFieldDefinition>
    {
        public DayOfWeekChain(DayOfWeekFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        public override IRangeFactoryLink BuildChain() => base.BuildChain()
            .AddNthDayOfWeek(FieldDefinition)
            .AddLastDayOfWeek(FieldDefinition);
    }
}
