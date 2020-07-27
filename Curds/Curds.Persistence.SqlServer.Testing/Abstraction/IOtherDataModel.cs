namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IOtherDataModel : IDataModel
    {
        TestEntity TestEntities { get; }
        OtherEntity Others { get; }
    }
}
