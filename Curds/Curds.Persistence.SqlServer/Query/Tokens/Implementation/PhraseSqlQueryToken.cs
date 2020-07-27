using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    internal class PhraseSqlQueryToken : CompositeSqlQueryToken
    {
        private static ConstantSqlQueryToken SpaceToken { get; } = new ConstantSqlQueryToken(" ");

        public List<ISqlQueryToken> Tokens { get; }

        public PhraseSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<ISqlQueryToken> tokens)
            : base(tokenFactory)
        {
            Tokens = tokens.ToList();
        }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens()
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (i != 0)
                    yield return SpaceToken;
                yield return Tokens[i];
            }
        }
    }
}
