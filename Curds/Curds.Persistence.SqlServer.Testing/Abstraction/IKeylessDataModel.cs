namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IKeylessDataModel : IDataModel
    {
        GenericToken Token { get; }
    }
}
