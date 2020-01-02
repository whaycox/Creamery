namespace Curds.Cron.RangeFactories.Abstraction
{
    public interface IRangeFactoryChain
    {
        IRangeFactoryLink BuildChain();
    }
}
