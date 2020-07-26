using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TokenListSqlQueryToken : BaseSqlQueryToken
    {
        public List<ISqlQueryToken> Tokens { get; }

        public bool IncludeGrouping { get; set; }
        public bool IncludeSeparators { get; set; } = true;

        public TokenListSqlQueryToken(IEnumerable<ISqlQueryToken> tokens)
        {
            Tokens = tokens.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitTokenList(this);
    }
}
