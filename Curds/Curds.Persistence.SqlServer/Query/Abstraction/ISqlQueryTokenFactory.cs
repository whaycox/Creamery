using System.Collections.Generic;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

    public interface ISqlQueryTokenFactory
    {
        ISqlQueryToken Phrase(params ISqlQueryToken[] tokenParams);
        ISqlQueryToken Keyword(SqlQueryKeyword keyword);
        ISqlQueryToken ObjectName(string name);
        ISqlQueryToken QualifiedObject(params string[] names);
        ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value);
        ISqlQueryToken ValueEntity(ISqlQueryParameterBuilder parameterBuilder, ValueEntity valueEntity);
        ISqlQueryToken GroupedList(IEnumerable<ISqlQueryToken> elements, bool useSeparators = true);
        ISqlQueryToken UngroupedList(IEnumerable<ISqlQueryToken> elements, bool useSeparators = true);

        ISqlQueryToken TableDefinition(ISqlTable table);
        ISqlQueryToken TableName(ISqlTable table, bool useAlias = true, bool useSqlName = true);
        ISqlQueryToken ColumnDefinition(ISqlColumn column);
        ISqlQueryToken ColumnName(ISqlColumn column, bool useAlias = true);
        ISqlQueryToken DbType(ISqlColumn column);

        ISqlQueryToken InsertedIdentityName(ISqlTable table);
        ISqlQueryToken JoinClause(ISqlJoinClause joinClause);
        ISqlQueryToken BooleanCombination(BooleanCombination combination, IEnumerable<ISqlQueryToken> elements);
        ISqlQueryToken BooleanComparison(BooleanComparison comparison, ISqlQueryToken left, ISqlQueryToken right);
        ISqlQueryToken ArithmeticOperation(ArithmeticOperation operation, ISqlQueryToken left, ISqlQueryToken right);
    }
}
