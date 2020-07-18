using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class JoinClauseSqlQueryToken : BaseSqlQueryToken
    {
        public IEnumerable<ISqlQueryToken> Clauses { get; }

        public JoinClauseSqlQueryToken(ISqlJoinClause joinClause)
        {
            Clauses = joinClause.Tokens;
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitJoinClause(this);
    }
}
