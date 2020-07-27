using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;

    internal class SqlQueryAliasBuilder : ISqlQueryAliasBuilder
    {
        private IAliasStrategy AliasStrategy { get; }

        private HashSet<string> Aliases { get; } = new HashSet<string>();

        public SqlQueryAliasBuilder(IAliasStrategy aliasStrategy)
        {
            AliasStrategy = aliasStrategy;
        }

        public string RegisterNewAlias(string objectName)
        {
            int disambiguator = 1;
            string currentAlias = AliasStrategy.GenerateAlias(objectName, disambiguator++);
            while (Aliases.Contains(currentAlias))
                currentAlias = AliasStrategy.GenerateAlias(objectName, disambiguator++);

            Aliases.Add(currentAlias);
            return currentAlias;
        }
    }
}
