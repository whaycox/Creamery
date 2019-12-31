namespace Curds.Cron.Abstraction
{
    public interface ICronRangeLink
    {
        ICronRangeLink Successor { get; }

        ICronRange HandleParse(string range);
    }
}
