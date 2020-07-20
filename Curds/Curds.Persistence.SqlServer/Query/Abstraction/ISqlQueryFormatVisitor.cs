﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Tokens.Implementation;

    public interface ISqlQueryFormatVisitor
    {
        void VisitLiteral(LiteralSqlQueryToken token);
        void VisitTokenList(TokenListSqlQueryToken token);
        void VisitSetValues(SetValuesSqlQueryToken token);
        void VisitValueEntities(ValueEntitiesSqlQueryToken token);
        void VisitValueEntity(ValueEntitySqlQueryToken token);
        void VisitJoinClause(JoinClauseSqlQueryToken token);
        void VisitBooleanCombination(BooleanCombinationSqlQueryToken token);
    }
}
