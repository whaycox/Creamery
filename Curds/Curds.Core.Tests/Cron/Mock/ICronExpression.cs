namespace Curds.Cron.Mock
{
    public class ICronExpression : ICronObject, Abstraction.ICronExpression
    {
        public string Expression => nameof(Expression);
    }
}
