namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class ColumnDefinitionSqlQueryToken : RedirectedSqlQueryToken
    {
        private ISqlColumn Column { get; }

        public ColumnDefinitionSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn column)
            : base(tokenFactory)
        {
            Column = column;
        }

        protected override ISqlQueryToken RedirectedToken() => TokenFactory.Phrase(
            TokenFactory.ColumnName(Column, false),
            TokenFactory.DbType(Column));
    }
}
