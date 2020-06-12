using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Query.Domain;
    using Domain;
    using Tokens.Implementation;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        public ISqlQueryToken ColumnList(IEnumerable<ISqlColumn> columns, bool includeDefinition) =>
            new ColumnListSqlQueryToken(columns)
            {
                IncludeDefinition = includeDefinition,
                IncludeGrouping = true,
            };

        public ISqlQueryToken SelectList(IEnumerable<ISqlColumn> columns) =>
            new ColumnListSqlQueryToken(columns)
            {
                IncludeDefinition = false,
                IncludeGrouping = false,
            };

        public ISqlQueryToken InsertedIdentityName(ISqlTable table) =>
            new InsertedIdentityColumnSqlQueryToken(table.Identity);

        public ISqlQueryToken Keyword(SqlQueryKeyword keyword) =>
            new KeywordSqlQueryToken(keyword);

        public ISqlQueryToken Phrase(params ISqlQueryToken[] tokens) =>
            new PhraseSqlQueryToken(tokens);

        public ISqlQueryToken QualifiedObjectName(ISqlTable table) =>
            new QualifiedObjectSqlQueryToken(table);

        public ISqlQueryToken QualifiedObjectName(ISqlColumn column) =>
            new QualifiedObjectSqlQueryToken(column);

        public ISqlQueryToken TemporaryIdentityName(ISqlTable table) =>
            new TemporaryIdentityTableNameSqlQueryToken(table);

        public ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value) => BuildParameterToken(parameterBuilder, name, value);
        private ParameterSqlQueryToken BuildParameterToken(ISqlQueryParameterBuilder parameterBuilder, string name, object value) =>
            new ParameterSqlQueryToken(parameterBuilder.RegisterNewParamater(name, value), value?.GetType());

        public ISqlQueryToken ValueEntities(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities) =>
            new ValueEntitiesSqlQueryToken(valueEntities.Select(valueEntity => BuildValueEntityToken(parameterBuilder, valueEntity)));
        private ValueEntitySqlQueryToken BuildValueEntityToken(ISqlQueryParameterBuilder parameterBuilder, ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(valueEntity.Values.Select(value => BuildParameterToken(parameterBuilder, value.Name, value.Content)));

        public ISqlQueryToken BooleanCombination(BooleanCombination combination, IEnumerable<ISqlQueryToken> elements) =>
            new BooleanCombinationSqlQueryToken(combination, elements);

        public ISqlQueryToken BooleanComparison(BooleanComparison comparison, ISqlQueryToken left, ISqlQueryToken right) =>
            new BooleanComparisonSqlQueryToken(comparison, left, right);

        public ISqlQueryToken ArithmeticOperation(ArithmeticOperation operation, ISqlQueryToken left, ISqlQueryToken right) =>
            new ArithmeticOperationSqlQueryToken(operation, left, right);

        public ISqlQueryToken SetValues(IEnumerable<ISqlQueryToken> setValueTokens) =>
            new SetValuesSqlQueryToken(setValueTokens);
    }
}
