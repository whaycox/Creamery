using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

    public interface ISqlQueryTokenFactory
    {
        ISqlQueryToken Keyword(SqlQueryKeyword keyword);
        ISqlQueryToken Phrase(IEnumerable<ISqlQueryToken> tokens);
        ISqlQueryToken Phrase(params ISqlQueryToken[] tokenParams);
        ISqlQueryToken TableDefinition(ISqlTable table);
        ISqlQueryToken TableName(ISqlTable table, bool useAlias = true, bool useSqlName = true);
        ISqlQueryToken ColumnDefinition(ISqlColumn column);
        ISqlQueryToken ColumnName(ISqlColumn column, bool useAlias = true);
        ISqlQueryToken InsertedIdentityName(ISqlTable table);
        ISqlQueryToken ColumnList(IEnumerable<ISqlColumn> columns, bool includeDefinition);
        ISqlQueryToken SelectList(IEnumerable<ISqlColumn> columns);
        ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value);
        ISqlQueryToken ValueEntities(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities);
        ISqlQueryToken SetValues(IEnumerable<ISqlQueryToken> setValueTokens);
        ISqlQueryToken JoinClause(ISqlJoinClause joinClause);
        ISqlQueryToken BooleanCombination(BooleanCombination combination, IEnumerable<ISqlQueryToken> elements);
        ISqlQueryToken BooleanComparison(BooleanComparison comparison, ISqlQueryToken left, ISqlQueryToken right);
        ISqlQueryToken ArithmeticOperation(ArithmeticOperation operation, ISqlQueryToken left, ISqlQueryToken right);
    }
}
