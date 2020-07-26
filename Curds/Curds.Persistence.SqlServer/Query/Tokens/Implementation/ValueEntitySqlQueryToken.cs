using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class ValueEntitySqlQueryToken : BaseSqlQueryToken
    {
        public List<ISqlQueryToken> Values { get; }

        public ValueEntitySqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlQueryParameterBuilder parameterBuilder,
            ValueEntity valueEntity)
        {
            Values = valueEntity
                .Values
                .Select(value => tokenFactory.Parameter(parameterBuilder, value.Name, value.Content))
                .ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitValueEntity(this);
    }
}
