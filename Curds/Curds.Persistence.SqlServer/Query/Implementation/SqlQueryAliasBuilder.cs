using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;

    internal class SqlQueryAliasBuilder : ISqlQueryAliasBuilder
    {
        private HashSet<string> Aliases { get; } = new HashSet<string>();

        public string RegisterNewAlias(string objectName)
        {
            int disambiguator = 1;
            string currentAlias = objectName;
            while (Aliases.Contains(currentAlias))
                currentAlias = Disambiguate(objectName, disambiguator++);

            Aliases.Add(currentAlias);
            return currentAlias;
        }
        private string Disambiguate(string objectName, int disambiguator) => $"{objectName}_{disambiguator}";
    }
}
