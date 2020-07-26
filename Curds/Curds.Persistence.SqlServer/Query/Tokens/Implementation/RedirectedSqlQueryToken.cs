using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class RedirectedSqlQueryToken : CompositeSqlQueryToken
    {
        public RedirectedSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
            : base(tokenFactory)
        { }

        protected sealed override IEnumerable<ISqlQueryToken> GenerateTokens() =>
            new[] { RedirectedToken() };
        protected abstract ISqlQueryToken RedirectedToken();
    }
}
