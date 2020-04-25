namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Domain;

    public class KeywordSqlQueryToken : LiteralSqlQueryToken
    {
        public override string Literal => Keyword.ToString();
        public SqlQueryKeyword Keyword { get; }

        public KeywordSqlQueryToken(SqlQueryKeyword keyword)
        {
            Keyword = keyword;
        }
    }
}
