using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Model.Abstraction;
    using Domain;
    using Tokens.Implementation;
    using Persistence.Abstraction;

    internal class SqlQueryTokenFactory : ISqlQueryTokenFactory
    {
        private ISqlQueryParameterBuilder ParameterBuilder { get; }

        public SqlQueryTokenFactory(ISqlQueryParameterBuilder parameterBuilder)
        {
            ParameterBuilder = parameterBuilder;
        }

        public ISqlQueryToken ColumnList(IEnumerable<IValueModel> values, bool includeDefinition) =>
            new ColumnListSqlQueryToken(values) 
            { 
                IncludeDefinition = includeDefinition,
                IncludeGrouping = true,
            };

        public ISqlQueryToken SelectList(IEnumerable<IValueModel> values) =>
            new ColumnListSqlQueryToken(values)
            {
                IncludeDefinition = false,
                IncludeGrouping = false,
            };

        public ISqlQueryToken InsertedIdentityName(IEntityModel entityModel) =>
            new InsertedIdentityColumnSqlQueryToken(entityModel.Identity);

        public ISqlQueryToken Keyword(SqlQueryKeyword keyword) =>
            new KeywordSqlQueryToken(keyword);

        public ISqlQueryToken Phrase(params ISqlQueryToken[] tokens) =>
            new PhraseSqlQueryToken(tokens);

        public ISqlQueryToken QualifiedObjectName(IEntityModel entityModel) =>
            new QualifiedObjectSqlQueryToken(entityModel);

        public ISqlQueryToken TemporaryIdentityName(IEntityModel entityModel) =>
            new TemporaryIdentityTableNameSqlQueryToken(entityModel);

        public ISqlQueryToken ValueEntities(IEnumerable<ValueEntity> valueEntities) =>
            new ValueEntitiesSqlQueryToken(valueEntities.Select(valueEntity => BuildValueEntityToken(valueEntity)));
        private ValueEntitySqlQueryToken BuildValueEntityToken(ValueEntity valueEntity) =>
            new ValueEntitySqlQueryToken(valueEntity.Values.Select(value => BuildValueToken(value)));
        private ParameterSqlQueryToken BuildValueToken(Value value) =>
            new ParameterSqlQueryToken(ParameterBuilder.RegisterNewParamater(value));
    }
}
