using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Model.Abstraction;
    using Tokens.Implementation;
    using Persistence.Abstraction;

    internal class SqlQueryWriter : ISqlQueryWriter
    {
        private ISqlQueryTokenFactory TokenFactory { get; }
        private ISqlQueryFormatter Formatter { get; }
        private ISqlQueryParameterBuilder ParameterBuilder { get; }

        public List<ISqlQueryToken> Tokens { get; } = new List<ISqlQueryToken>();

        public SqlQueryWriter(
            ISqlQueryTokenFactory tokenFactory,
            ISqlQueryFormatter formatter,
            ISqlQueryParameterBuilder parameterBuilder)
        {
            TokenFactory = tokenFactory;
            Formatter = formatter;
            ParameterBuilder = parameterBuilder;
        }

        public SqlCommand Flush()
        {
            SqlCommand query = new SqlCommand(Formatter.FormatTokens(Tokens));
            Tokens.Clear();
            query.Parameters.AddRange(ParameterBuilder.Flush());
            return query;
        }

        public void CreateTemporaryIdentityTable(IEntityModel entityModel) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.CREATE),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(entityModel),
                TokenFactory.ColumnList(new[] { entityModel.Identity }, true)));

        public void OutputIdentitiesToTemporaryTable(IEntityModel entityModel) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
                TokenFactory.InsertedIdentityName(entityModel),
                TokenFactory.Keyword(SqlQueryKeyword.INTO),
                TokenFactory.TemporaryIdentityName(entityModel)));

        public void SelectTemporaryIdentityTable(IEntityModel entityModel)
        {
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                    TokenFactory.ColumnList(new[] { entityModel.Identity }, false)));
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.TemporaryIdentityName(entityModel)));
        }

        public void DropTemporaryIdentityTable(IEntityModel entityModel) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.DROP),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(entityModel)));

        public void Insert(IEntityModel entityModel) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.INSERT),
                TokenFactory.QualifiedObjectName(entityModel),
                TokenFactory.ColumnList(entityModel.NonIdentities, false)));

        public void ValueEntities(IEnumerable<ValueEntity> entities)
        {
            Tokens.Add(TokenFactory.Keyword(SqlQueryKeyword.VALUES));
            Tokens.Add(TokenFactory.ValueEntities(entities));
        }

        public void Select(List<IValueModel> values) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                TokenFactory.SelectList(values)));

        public void From(IEntityModel entityModel) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.FROM),
                TokenFactory.QualifiedObjectName(entityModel)));
    }
}
