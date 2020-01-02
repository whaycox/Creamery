namespace Curds.Cron.Abstraction
{
    public interface ICronRangeFactory
    {
        ICronRange Parse(string range);
    }

    public interface ICronRangeFactory<TFieldDefinition> : ICronRangeFactory
        where TFieldDefinition : ICronFieldDefinition
    { }
}
