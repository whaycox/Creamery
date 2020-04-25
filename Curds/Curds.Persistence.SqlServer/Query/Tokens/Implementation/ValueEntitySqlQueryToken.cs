using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class ValueEntitySqlQueryToken : BaseSqlQueryToken
    {
        public List<ParameterSqlQueryToken> Values { get; }

        public ValueEntitySqlQueryToken(IEnumerable<ParameterSqlQueryToken> values)
        {
            Values = values.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitValueEntity(this);
    }
}
