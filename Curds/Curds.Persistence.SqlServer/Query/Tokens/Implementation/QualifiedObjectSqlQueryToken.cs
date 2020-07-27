using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class QualifiedObjectSqlQueryToken : CompositeSqlQueryToken
    {
        private static ConstantSqlQueryToken QualifierToken { get; } = new ConstantSqlQueryToken(".");

        public List<string> Names { get; }

        public QualifiedObjectSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<string> names)
            : base(tokenFactory)
        {
            Names = names
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList();
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            for (int i = 0; i < Names.Count; i++)
            {
                if (i != 0)
                    yield return QualifierToken;
                yield return TokenFactory.ObjectName(Names[i]);
            }
        }
    }
}
