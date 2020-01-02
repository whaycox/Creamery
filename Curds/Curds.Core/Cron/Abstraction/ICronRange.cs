namespace Curds.Cron.Abstraction
{
    public interface ICronRange : ICronObject
    { }

    public interface ICronRange<TFieldDefinition> : ICronRange
        where TFieldDefinition : ICronFieldDefinition
    { }
}
