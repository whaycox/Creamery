namespace Curds.Cron.Abstraction
{
    public interface ICronExpressionFactory
    {
        ICronExpression Parse(string expression);
    }
}
