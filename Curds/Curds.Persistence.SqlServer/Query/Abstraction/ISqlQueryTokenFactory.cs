using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;
    using Model.Domain;

    public interface ISqlQueryTokenFactory
    {
        ISqlQueryToken Keyword(SqlQueryKeyword keyword);
        ISqlQueryToken Phrase(params ISqlQueryToken[] tokens);
        ISqlQueryToken TemporaryIdentityName(Table table);
        ISqlQueryToken InsertedIdentityName(Table table);
        ISqlQueryToken QualifiedObjectName(Table table);
        ISqlQueryToken ColumnList(IEnumerable<Column> columns, bool includeDefinition);
        ISqlQueryToken SelectList(IEnumerable<Column> columns);
        ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities);
    }
}
