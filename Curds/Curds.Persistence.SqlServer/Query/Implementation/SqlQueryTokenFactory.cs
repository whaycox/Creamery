using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Query.Domain;
    using Tokens.Implementation;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        public ISqlQueryToken ColumnList(IEnumerable<ISqlColumn> columns, bool includeDefinition) =>
            new TokenListSqlQueryToken(
                this,
                columns.Select(column => ColumnToken(column, includeDefinition)))
            {
                IncludeGrouping = true,
            };
        private ISqlQueryToken ColumnToken(ISqlColumn column, bool includeDefinition) =>
            includeDefinition ?
                ColumnDefinition(column) :
                ColumnName(column, false);

        public ISqlQueryToken SelectList(IEnumerable<ISqlColumn> columns) =>
            new TokenListSqlQueryToken(
                this,
                columns.Select(column => ColumnName(column, true)));

        public ISqlQueryToken InsertedIdentityName(ISqlTable table) =>
            new InsertedIdentityColumnSqlQueryToken(
                this,
                table.Identity);

        public ISqlQueryToken Keyword(SqlQueryKeyword keyword) =>
            new KeywordSqlQueryToken(keyword);

        public ISqlQueryToken Phrase(IEnumerable<ISqlQueryToken> tokens) =>
            new PhraseSqlQueryToken(
                this,
                tokens);

        public ISqlQueryToken Phrase(params ISqlQueryToken[] tokenParams) =>
            new PhraseSqlQueryToken(
                this,
                tokenParams);

        public ISqlQueryToken TableName(ISqlTable table, bool useAlias, bool useSqlName) =>
            new TableNameSqlQueryToken(
                this,
                table)
            {
                UseAlias = useAlias,
                UseSqlName = useSqlName,
            };

        public ISqlQueryToken ColumnDefinition(ISqlColumn column) =>
            new ColumnDefinitionSqlQueryToken(
                this,
                column);

        public ISqlQueryToken ColumnName(ISqlColumn column, bool useAlias) =>
            new ColumnNameSqlQueryToken(
                this,
                column)
            {
                UseAlias = useAlias
            };

        public ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value) => BuildParameterToken(parameterBuilder, name, value);
        private ParameterSqlQueryToken BuildParameterToken(ISqlQueryParameterBuilder parameterBuilder, string name, object value) =>
            new ParameterSqlQueryToken(parameterBuilder.RegisterNewParamater(name, value), value?.GetType());

        public ISqlQueryToken ValueEntities(ISqlQueryParameterBuilder parameterBuilder, IEnumerable<ValueEntity> valueEntities) =>
            new ValueEntitiesSqlQueryToken(
                this,
                valueEntities.Select(valueEntity => BuildValueEntityToken(parameterBuilder, valueEntity)));
        private ValueEntitySqlQueryToken BuildValueEntityToken(ISqlQueryParameterBuilder parameterBuilder, ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(
                this,
                valueEntity.Values.Select(value => BuildParameterToken(parameterBuilder, value.Name, value.Content)));

        public ISqlQueryToken SetValues(IEnumerable<ISqlQueryToken> setValueTokens) =>
            new SetValuesSqlQueryToken(
                this,
                setValueTokens);

        public ISqlQueryToken JoinClause(ISqlJoinClause joinClause) =>
            new JoinClauseSqlQueryToken(
                this,
                joinClause);

        public ISqlQueryToken BooleanCombination(BooleanCombination combination, IEnumerable<ISqlQueryToken> elements) =>
            new BooleanCombinationSqlQueryToken(
                this,
                combination,
                elements);

        public ISqlQueryToken BooleanComparison(BooleanComparison comparison, ISqlQueryToken left, ISqlQueryToken right) =>
            new BooleanComparisonSqlQueryToken(
                this,
                comparison,
                left,
                right);

        public ISqlQueryToken ArithmeticOperation(ArithmeticOperation operation, ISqlQueryToken left, ISqlQueryToken right) =>
            new ArithmeticOperationSqlQueryToken(
                this,
                operation,
                left,
                right);
    }
}
