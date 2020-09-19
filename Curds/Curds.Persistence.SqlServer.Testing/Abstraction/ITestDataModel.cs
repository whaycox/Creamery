namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ITestDataModel : IDataModel
    {
        TestEntity Test { get; }
        OtherEntity Other { get; }
        TestEnumEntity Enums { get; }

        Parent Parents { get; }
        Child Children { get; }
    }
}
