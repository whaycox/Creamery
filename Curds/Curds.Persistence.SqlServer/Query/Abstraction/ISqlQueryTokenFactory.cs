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
        ISqlQueryToken TemporaryIdentityName(IEntityModel entityModel);
        ISqlQueryToken InsertedIdentityName(IEntityModel entityModel);
        ISqlQueryToken QualifiedObjectName(IEntityModel entityModel);
        ISqlQueryToken ColumnList(IEnumerable<IValueModel> values, bool includeDefinition);
        ISqlQueryToken SelectList(IEnumerable<IValueModel> values);
        ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities);
    }
}
