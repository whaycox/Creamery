namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ISqlConnectionStringFactory
    {
        string Build(SqlConnectionInformation connectionInformation);
    }
}
