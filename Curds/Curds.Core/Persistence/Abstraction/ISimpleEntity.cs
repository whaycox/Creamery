namespace Curds.Persistence.Abstraction
{
    public interface ISimpleEntity : IEntity
    {
        int ID { get; set; }
    }
}
