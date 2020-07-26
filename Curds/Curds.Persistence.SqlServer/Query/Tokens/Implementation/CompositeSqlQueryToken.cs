using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class CompositeSqlQueryToken : BaseSqlQueryToken
    {
        protected ISqlQueryTokenFactory TokenFactory { get; }

        public CompositeSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
        {
            TokenFactory = tokenFactory;
        }

        public sealed override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            foreach (ISqlQueryToken token in GenerateTokens())
                token.AcceptFormatVisitor(visitor);
        }
        protected abstract IEnumerable<ISqlQueryToken> GenerateTokens();
    }
}
