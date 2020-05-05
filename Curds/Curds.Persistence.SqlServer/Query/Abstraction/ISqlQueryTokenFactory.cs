using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;
    using Model.Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;

    public interface ISqlQueryTokenFactory
    {
        ISqlQueryToken Keyword(SqlQueryKeyword keyword);
        ISqlQueryToken Phrase(params ISqlQueryToken[] tokens);
        ISqlQueryToken TemporaryIdentityName(ISqlTable table);
        ISqlQueryToken InsertedIdentityName(ISqlTable table);
        ISqlQueryToken QualifiedObjectName(ISqlTable table);
        ISqlQueryToken QualifiedObjectName(ISqlColumn column);
        ISqlQueryToken ColumnList(IEnumerable<ISqlColumn> columns, bool includeDefinition);
        ISqlQueryToken SelectList(IEnumerable<ISqlColumn> columns);
        ISqlQueryToken Parameter(string name, object value);
        ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities);
        ISqlQueryToken UniverseFilter(ISqlUniverseFilter filter);
    }
}
