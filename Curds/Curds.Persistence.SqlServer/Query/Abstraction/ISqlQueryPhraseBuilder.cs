using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Query.Abstraction;
    using Query.Domain;

    public interface ISqlQueryPhraseBuilder
    {
        ISqlQueryToken CreateTableToken(ISqlTable table);
        ISqlQueryToken DropTableToken(ISqlTable table);

        ISqlQueryToken OutputToTemporaryIdentityToken(ISqlTable table);

        ISqlQueryToken InsertToTableToken(ISqlTable table);
        ISqlQueryToken ValueEntitiesToken(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities);
        ISqlQueryToken SelectColumnsToken(IEnumerable<ISqlColumn> columns);
        ISqlQueryToken FromTableToken(ISqlTable table);
        ISqlQueryToken JoinTableToken(ISqlJoinClause joinClause);
        ISqlQueryToken DeleteTableToken(ISqlTable table);
        ISqlQueryToken UpdateTableToken(ISqlTable table, IEnumerable<ISqlQueryToken> setValueTokens);
    }
}
