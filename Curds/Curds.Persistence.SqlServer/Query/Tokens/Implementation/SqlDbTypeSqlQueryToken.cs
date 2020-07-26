using System;
using System.Data;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class SqlDbTypeSqlQueryToken : RedirectedSqlQueryToken
    {
        private ISqlColumn Column { get; }

        private ISqlQueryToken TypeToken
        {
            get
            {
                switch (Column.Type)
                {
                    case SqlDbType.Int:
                        return TokenFactory.Keyword(SqlQueryKeyword.INT);
                    default:
                        throw new InvalidOperationException($"Unsupported data type: {Column.Type}");
                }
            }
        }

        public SqlDbTypeSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn column)
            : base(tokenFactory)
        {
            Column = column;
        }

        protected override ISqlQueryToken RedirectedToken() => TokenFactory.Phrase(
            TypeToken,
            TokenFactory.Keyword(SqlQueryKeyword.NOT),
            TokenFactory.Keyword(SqlQueryKeyword.NULL));
    }
}
