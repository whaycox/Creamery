namespace Curds.Cron.RangeLinkFactories.Implementation
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
