using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using Model.Domain;
    using Tokens.Implementation;

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

        public void CreateTemporaryIdentityTable(Table table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.CREATE),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(table),
                TokenFactory.ColumnList(table.IdentityColumns, true)));

        public void OutputIdentitiesToTemporaryTable(Table table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.OUTPUT),
                TokenFactory.InsertedIdentityName(table),
                TokenFactory.Keyword(SqlQueryKeyword.INTO),
                TokenFactory.TemporaryIdentityName(table)));

        public void SelectTemporaryIdentityTable(Table table)
        {
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                    TokenFactory.ColumnList(table.IdentityColumns, false)));
            Tokens.Add(
                TokenFactory.Phrase(
                    TokenFactory.Keyword(SqlQueryKeyword.FROM),
                    TokenFactory.TemporaryIdentityName(table)));
        }

        public void DropTemporaryIdentityTable(Table table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.DROP),
                TokenFactory.Keyword(SqlQueryKeyword.TABLE),
                TokenFactory.TemporaryIdentityName(table)));

        public void Insert(Table table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.INSERT),
                TokenFactory.QualifiedObjectName(table),
                TokenFactory.ColumnList(table.NotIdentityColumns, false)));

        public void ValueEntities(IEnumerable<ValueEntity> entities)
        {
            Tokens.Add(TokenFactory.Keyword(SqlQueryKeyword.VALUES));
            Tokens.Add(TokenFactory.ValueEntities(entities));
        }

        public void Select(List<Column> columns) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.SELECT),
                TokenFactory.SelectList(columns)));

        public void From(Table table) => Tokens.Add(
            TokenFactory.Phrase(
                TokenFactory.Keyword(SqlQueryKeyword.FROM),
                TokenFactory.QualifiedObjectName(table)));
    }
}
