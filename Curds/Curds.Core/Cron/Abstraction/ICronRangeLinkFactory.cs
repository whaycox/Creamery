namespace Curds.Cron.Abstraction
{
    public interface ICronRangeLinkFactory
    {
        ICronRangeLink StartOfChain { get; }
    }
}
