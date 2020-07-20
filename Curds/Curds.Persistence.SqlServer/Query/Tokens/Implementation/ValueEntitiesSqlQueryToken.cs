using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class ValueEntitiesSqlQueryToken : BaseSqlQueryToken
    {
        public List<ValueEntitySqlQueryToken> Entities { get; }

        public ValueEntitiesSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            IEnumerable<ValueEntitySqlQueryToken> entities)
            : base(tokenFactory)
        {
            Entities = entities.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitValueEntities(this);
    }
}
