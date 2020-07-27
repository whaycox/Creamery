namespace Curds.Persistence.Query.Abstraction
{
    public interface ISqlQueryAliasBuilder
    {
        string RegisterNewAlias(string objectName);
    }
}
