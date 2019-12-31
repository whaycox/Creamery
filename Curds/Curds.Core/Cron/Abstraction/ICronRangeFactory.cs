namespace Curds.Cron.Abstraction
{
    public interface ICronRangeFactory
    {
        ICronRange Parse(string range);
    }
}
