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

        public void CreateTemporaryIdentityTable(ISqlTable table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.CREATE),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(table),
                TokenFactory.ColumnList(new[] { table.Identity }, true)));

        public void OutputIdentitiesToTemporaryTable(ISqlTable table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
                TokenFactory.InsertedIdentityName(table),
                TokenFactory.Keyword(SqlQueryKeyword.INTO),
                TokenFactory.TemporaryIdentityName(table)));

        public void SelectTemporaryIdentityTable(ISqlTable table)
        {
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                    TokenFactory.ColumnList(new[] { table.Identity }, false)));
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.TemporaryIdentityName(table)));
        }

        public void DropTemporaryIdentityTable(ISqlTable table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.DROP),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(table)));

        public void Insert(ISqlTable table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.INSERT),
                TokenFactory.QualifiedObjectName(table),
                TokenFactory.ColumnList(table.NonIdentities, false)));

        public void ValueEntities(IEnumerable<ValueEntity> entities)
        {
            Tokens.Add(TokenFactory.Keyword(SqlQueryKeyword.VALUES));
            Tokens.Add(TokenFactory.ValueEntities(entities));
        }

        public void Select(IList<ISqlColumn> columns) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                TokenFactory.SelectList(columns)));

        public void From(ISqlUniverse universe)
        {
            foreach(ISqlTable table in universe.Tables)
                Tokens.Add(TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.QualifiedObjectName(table)));

            foreach (ISqlUniverseFilter filter in universe.Filters)
                Tokens.Add(TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.WHERE),
                    TokenFactory.UniverseFilter(filter)));
        }
    }
}
