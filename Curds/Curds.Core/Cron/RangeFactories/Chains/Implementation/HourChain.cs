namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class HourChain : BaseChain<HourFieldDefinition>
    {
        public HourChain(HourFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
