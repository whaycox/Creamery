using System.Collections.Generic;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Query.Domain;
    using Tokens.Implementation;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        public ISqlQueryToken Phrase(params ISqlQueryToken[] tokenParams) =>
            new PhraseSqlQueryToken(
                this,
                tokenParams);

        public ISqlQueryToken Keyword(SqlQueryKeyword keyword) =>
            new KeywordSqlQueryToken(keyword);

        public ISqlQueryToken ObjectName(string name) =>
            new ObjectNameSqlQueryToken(name);

        public ISqlQueryToken QualifiedObject(params string[] names) =>
            new QualifiedObjectSqlQueryToken(
                this,
                names);

        public ISqlQueryToken Parameter(ISqlQueryParameterBuilder parameterBuilder, string name, object value) => BuildParameterToken(parameterBuilder, name, value);
        private ParameterSqlQueryToken BuildParameterToken(ISqlQueryParameterBuilder parameterBuilder, string name, object value) =>
            new ParameterSqlQueryToken(parameterBuilder.RegisterNewParamater(name, value), value?.GetType());

        public ISqlQueryToken ValueEntity(ISqlQueryParameterBuilder parameterBuilder, ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(
                this,
                parameterBuilder,
                valueEntity);

        public ISqlQueryToken GroupedList(IEnumerable<ISqlQueryToken> elements, bool useSeparators) =>
            new TokenListSqlQueryToken(elements)
            {
                IncludeGrouping = true,
                IncludeSeparators = useSeparators,
            };

        public ISqlQueryToken UngroupedList(IEnumerable<ISqlQueryToken> elements, bool useSeparators) =>
            new TokenListSqlQueryToken(elements)
            {
                IncludeGrouping = false,
                IncludeSeparators = useSeparators,
            };

        public ISqlQueryToken TableDefinition(ISqlTable table) =>
            new TableDefinitionSqlQueryToken(
                this,
                table);

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

        public ISqlQueryToken DbType(ISqlColumn column) =>
            new SqlDbTypeSqlQueryToken(
                this,
                column);

        public ISqlQueryToken InsertedIdentityName(ISqlTable table) =>
            new InsertedIdentityColumnSqlQueryToken(
                this,
                table.Identity);

        public ISqlQueryToken JoinClause(ISqlJoinClause joinClause) =>
            new TokenListSqlQueryToken(joinClause.Tokens)
            {
                IncludeSeparators = false,
            };

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
