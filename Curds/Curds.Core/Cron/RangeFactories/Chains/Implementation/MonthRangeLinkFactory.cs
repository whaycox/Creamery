namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MonthRangeLinkFactory : BaseRangeLinkFactory<MonthFieldDefinition>, IMonthRangeLinkFactory
    {
        public MonthRangeLinkFactory(MonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
