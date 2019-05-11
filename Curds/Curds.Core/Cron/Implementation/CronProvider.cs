namespace Curds.Cron.Implementation
{
    using Abstraction;

    public class CronProvider : ICron
    {
        public Domain.CronExpression Build(string cronExpression) => new CronExpression(cronExpression);
    }
}
