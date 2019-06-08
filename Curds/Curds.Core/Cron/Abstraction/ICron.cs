namespace Curds.Cron.Abstraction
{
    public interface ICron
    {
        ICronExpression Build(string cronExpression);
    }
}
