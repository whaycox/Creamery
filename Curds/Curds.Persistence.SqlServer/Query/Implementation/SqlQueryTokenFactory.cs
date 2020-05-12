using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Tokens.Implementation;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        private ISqlQueryParameterBuilder ParameterBuilder { get; }

        public SqlQueryTokenFactory(ISqlQueryParameterBuilder parameterBuilder)
        {
            ParameterBuilder = parameterBuilder;
        }

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

        public ISqlQueryToken Parameter(string name, object value) => BuildParameterToken(name, value);
        private ParameterSqlQueryToken BuildParameterToken(string name, object value) =>
            new ParameterSqlQueryToken(ParameterBuilder.RegisterNewParamater(name, value));

        public ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities) =>
            new ValueEntitiesSqlQueryToken(valueEntities.Select(valueEntity => BuildValueEntityToken(valueEntity)));
        private ValueEntitySqlQueryToken BuildValueEntityToken(ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(valueEntity.Values.Select(value => BuildParameterToken(value.Name, value.Content)));

        public ISqlQueryToken UniverseFilter(ISqlUniverseFilter filter) =>
            new BooleanSqlQueryToken(filter.Operation, filter.Left(this), filter.Right(this));
    }
}
