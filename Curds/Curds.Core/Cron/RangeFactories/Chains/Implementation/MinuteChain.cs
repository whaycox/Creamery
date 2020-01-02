namespace Curds.Cron.RangeFactories.Chains.Implementation
{
    using Abstraction;
    using FieldDefinitions.Implementation;

    internal class MinuteChain : BaseChain<MinuteFieldDefinition>
    {
        public MinuteChain(MinuteFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }
    }
}
