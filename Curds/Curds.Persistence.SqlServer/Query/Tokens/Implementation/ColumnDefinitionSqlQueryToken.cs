using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class ColumnDefinitionSqlQueryToken : BaseSqlQueryToken
    {
        private ISqlColumn Column { get; }

        public ColumnDefinitionSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn column)
            : base(tokenFactory)
        {
            Column = column;
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            List<ISqlQueryToken> definitionTokens = new List<ISqlQueryToken>();
            definitionTokens.Add(TokenFactory.ColumnName(Column, false));
            definitionTokens.Add(TypeToken());
            definitionTokens.Add(TokenFactory.Keyword(SqlQueryKeyword.NOT));
            definitionTokens.Add(TokenFactory.Keyword(SqlQueryKeyword.NULL));

            TokenFactory
                .Phrase(definitionTokens)
                .AcceptFormatVisitor(visitor);
        }
        private ISqlQueryToken TypeToken()
        {
            switch (Column.Type)
            {
                case SqlDbType.Int:
                    return TokenFactory.Keyword(SqlQueryKeyword.INT);
                default:
                    throw new InvalidOperationException($"Unsupported column type: {Column.Type}");
            }
        }
    }
}
