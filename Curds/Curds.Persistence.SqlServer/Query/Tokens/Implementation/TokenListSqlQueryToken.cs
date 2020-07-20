using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TokenListSqlQueryToken : BaseSqlQueryToken
    {
        public List<ISqlQueryToken> Tokens { get; }

        public bool IncludeGrouping { get; set; }

        public TokenListSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<ISqlQueryToken> tokens)
            : base(tokenFactory)
        {
            Tokens = tokens.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitTokenList(this);
    }
}
