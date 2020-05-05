using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Tokens.Implementation;

    public interface ISqlQueryFormatVisitor
    {
        void VisitLiteral(LiteralSqlQueryToken token);
        void VisitColumnList(ColumnListSqlQueryToken token);
        void VisitValueEntities(ValueEntitiesSqlQueryToken token);
        void VisitValueEntity(ValueEntitySqlQueryToken token);
        void VisitBoolean(BooleanSqlQueryToken token);
    }
}
