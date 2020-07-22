namespace Curds.Persistence.Query.AliasStrategies.Implementation
{
    internal class SimpleAliasStrategy : BaseAliasStrategy
    {
        public override string GenerateAlias(string objectName, int disambiguator) => $"{objectName}_{disambiguator}";
    }
}
