using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Domain;
    using Domain;
    using Tokens.Implementation;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        private ISqlQueryParameterBuilder ParameterBuilder { get; }

        public SqlQueryTokenFactory(ISqlQueryParameterBuilder parameterBuilder)
        {
            ParameterBuilder = parameterBuilder;
        }

        public ISqlQueryToken ColumnList(IEnumerable<Column> columns, bool includeDefinition) =>
            new ColumnListSqlQueryToken(columns) 
            { 
                IncludeDefinition = includeDefinition,
                IncludeGrouping = true,
            };

        public ISqlQueryToken SelectList(IEnumerable<Column> columns) =>
            new ColumnListSqlQueryToken(columns)
            {
                IncludeDefinition = false,
                IncludeGrouping = false,
            };

        public ISqlQueryToken InsertedIdentityName(Table table) =>
            new InsertedIdentityColumnSqlQueryToken(table.IdentityColumn);

        public ISqlQueryToken Keyword(SqlQueryKeyword keyword) =>
            new KeywordSqlQueryToken(keyword);

        public ISqlQueryToken Phrase(params ISqlQueryToken[] tokens) =>
            new PhraseSqlQueryToken(tokens);

        public ISqlQueryToken QualifiedObjectName(Table table) =>
            new QualifiedObjectSqlQueryToken(table);

        public ISqlQueryToken TemporaryIdentityName(Table table) =>
            new TemporaryIdentityTableNameSqlQueryToken(table);

        public ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities) =>
            new ValueEntitiesSqlQueryToken(valueEntities.Select(valueEntity => BuildValueEntityToken(valueEntity)));
        private ValueEntitySqlQueryToken BuildValueEntityToken(ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(valueEntity.Values.Select(value => BuildValueToken(value)));
        private ParameterSqlQueryToken BuildValueToken(Value value) =>
            new ParameterSqlQueryToken(ParameterBuilder.RegisterNewParamater(value));
    }
}
