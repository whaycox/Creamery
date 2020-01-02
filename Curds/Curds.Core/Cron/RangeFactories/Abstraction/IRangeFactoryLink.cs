namespace Curds.Cron.RangeFactories.Abstraction
{
    using Cron.Abstraction;

    public interface IRangeFactoryLink
    {
        IRangeFactoryLink Successor { get; }

        ICronRange HandleParse(string range);
    }
}
