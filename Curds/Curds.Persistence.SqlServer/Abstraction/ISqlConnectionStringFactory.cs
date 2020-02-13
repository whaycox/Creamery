namespace Curds.Persistence.Abstraction
{
    using Domain;

    internal interface ISqlConnectionStringFactory
    {
        string Build(SqlConnectionInformation connectionInformation);
    }
}
