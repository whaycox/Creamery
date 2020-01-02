namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MonthChain : BaseChain<MonthFieldDefinition>
    {
        public MonthChain(MonthFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
