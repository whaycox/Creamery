namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ITestDataModel : IDataModel
    {
        TestEntity Test { get; }
        OtherEntity Other { get; }

        Parent Parents { get; }
        Child Children { get; }
    }
}
