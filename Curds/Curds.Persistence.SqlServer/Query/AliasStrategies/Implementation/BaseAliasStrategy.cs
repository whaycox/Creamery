namespace Curds.Persistence.Query.AliasStrategies.Implementation
{
    using Abstraction;

    internal abstract class BaseAliasStrategy : IAliasStrategy
    {
        public abstract string GenerateAlias(string objectName, int disambiguator);
    }
}
