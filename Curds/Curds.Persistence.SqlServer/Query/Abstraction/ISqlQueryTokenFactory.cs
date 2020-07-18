using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

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
        ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value);
        ISqlQueryToken ValueEntities(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities);
        ISqlQueryToken SetValues(IEnumerable<ISqlQueryToken> setValueTokens);
        ISqlQueryToken JoinClause(ISqlJoinClause joinClause);
        ISqlQueryToken BooleanCombination(BooleanCombination combination, IEnumerable<ISqlQueryToken> elements);
        ISqlQueryToken BooleanComparison(BooleanComparison comparison, ISqlQueryToken left, ISqlQueryToken right);
        ISqlQueryToken ArithmeticOperation(ArithmeticOperation operation, ISqlQueryToken left, ISqlQueryToken right);
    }
}
