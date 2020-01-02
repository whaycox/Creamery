namespace Curds.Cron.Abstraction
{
    public interface ICronFieldFactory
    {
        ICronField Parse(string field);
    }

    public interface ICronFieldFactory<TFieldDefinition> : ICronFieldFactory
        where TFieldDefinition : ICronFieldDefinition
    { }
}
