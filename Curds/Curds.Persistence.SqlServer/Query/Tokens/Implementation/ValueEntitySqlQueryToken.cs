using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class ValueEntitySqlQueryToken : BaseSqlQueryToken
    {
        public List<ParameterSqlQueryToken> Values { get; }

        public ValueEntitySqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<ParameterSqlQueryToken> values)
            : base(tokenFactory)
        {
            Values = values.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitValueEntity(this);
    }
}
