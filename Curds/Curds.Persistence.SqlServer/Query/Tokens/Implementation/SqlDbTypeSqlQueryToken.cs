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
                    case SqlDbType.Bit:
                        return TokenFactory.Keyword(SqlQueryKeyword.BIT);
                    case SqlDbType.TinyInt:
                        return TokenFactory.Keyword(SqlQueryKeyword.TINYINT);
                    case SqlDbType.SmallInt:
                        return TokenFactory.Keyword(SqlQueryKeyword.SMALLINT);
                    case SqlDbType.Int:
                        return TokenFactory.Keyword(SqlQueryKeyword.INT);
                    case SqlDbType.BigInt:
                        return TokenFactory.Keyword(SqlQueryKeyword.BIGINT);
                    case SqlDbType.DateTime:
                        return TokenFactory.Keyword(SqlQueryKeyword.DATETIME);
                    case SqlDbType.DateTimeOffset:
                        return TokenFactory.Keyword(SqlQueryKeyword.DATETIMEOFFSET);
                    case SqlDbType.Float:
                        return TokenFactory.Keyword(SqlQueryKeyword.FLOAT);
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
            NullabilityToken);
        private ISqlQueryToken NullabilityToken => TokenFactory.Phrase(
            TokenFactory.Keyword(SqlQueryKeyword.NOT),
            TokenFactory.Keyword(SqlQueryKeyword.NULL));
    }
}
