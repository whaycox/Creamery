namespace Curds.Persistence.Query.Abstraction
{
    public interface IAliasStrategy
    {
        string GenerateAlias(string objectName, int disambiguator);
    }
}
