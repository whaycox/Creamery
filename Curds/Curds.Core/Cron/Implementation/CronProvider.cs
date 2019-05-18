namespace Curds.Cron.Implementation
{
    using Abstraction;

    public class CronProvider : ICron
    {
        public ICronExpression Build(string cronExpression) => new CronExpression(cronExpression);
    }
}
