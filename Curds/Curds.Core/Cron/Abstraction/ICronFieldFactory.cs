namespace Curds.Cron.Abstraction
{
    public interface ICronFieldFactory
    {
        ICronField Parse(string field);
    }
}
