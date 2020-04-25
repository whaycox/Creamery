using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class ValueEntitiesSqlQueryToken : BaseSqlQueryToken
    {
        public List<ValueEntitySqlQueryToken> Entities { get; }

        public ValueEntitiesSqlQueryToken(IEnumerable<ValueEntitySqlQueryToken> entities)
        {
            Entities = entities.ToList();
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitValueEntities(this);
    }
}
