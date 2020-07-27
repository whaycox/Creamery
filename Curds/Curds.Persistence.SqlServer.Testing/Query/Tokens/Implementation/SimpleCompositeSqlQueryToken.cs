using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class SimpleCompositeSqlQueryToken : CompositeSqlQueryToken
    {
        public List<ISqlQueryToken> GeneratedTokens { get; } = new List<ISqlQueryToken>();

        public SimpleCompositeSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
            : base(tokenFactory)
        { }

        protected override IEnumerable<ISqlQueryToken> GenerateTokens() => GeneratedTokens;
    }
}
