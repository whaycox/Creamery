namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IWrongEntityModel : IDataModel
    {
        TestEntity Test { get; }
        WrongEntity Wrong { get; }
    }

    public class WrongEntity
    { }
}
