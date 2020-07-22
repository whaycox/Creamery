using System.Text;
using System.Text.RegularExpressions;

namespace Curds.Persistence.Query.AliasStrategies.Implementation
{
    internal class AbbreviatedAliasStrategy : BaseAliasStrategy
    {
        private static Regex AbbreviationSplitter = new Regex("(?=[A-Z])", RegexOptions.Compiled);

        public override string GenerateAlias(string objectName, int disambiguator)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string chunk in AbbreviationSplitter.Split(objectName))
                if (!string.IsNullOrWhiteSpace(chunk))
                    builder.Append(chunk[0]);
            builder.Append(disambiguator.ToString());
            return builder.ToString();
        }
    }
}
