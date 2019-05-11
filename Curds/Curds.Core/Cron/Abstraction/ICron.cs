namespace Curds.Cron.Abstraction
{
    using Domain;

    public interface ICron
    {
        CronExpression Build(string cronExpression);
    }
}
