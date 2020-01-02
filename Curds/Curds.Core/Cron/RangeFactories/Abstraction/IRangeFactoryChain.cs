namespace Curds.Cron.RangeFactories.Abstraction
{
    using Cron.Abstraction;

    public interface IRangeFactoryChain
    {
        IRangeFactoryLink BuildChain();
    }

    public interface IRangeFactoryChain<TFieldDefinition> : IRangeFactoryChain
        where TFieldDefinition : ICronFieldDefinition
    { }
}
