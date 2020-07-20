namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class ColumnNameSqlQueryToken : BaseObjectNameSqlQueryToken
    {
        public ISqlColumn Column { get; }

        public ColumnNameSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn column)
            : base(tokenFactory)
        {
            Column = column;
        }

        protected override ISqlQueryToken GenerateNameToken() =>
            UseAlias ?
                new QualifiedObjectSqlQueryToken(
                    TokenFactory,
                    new ObjectNameSqlQueryToken(Column.Table.Alias),
                    new ObjectNameSqlQueryToken(Column.Name)) :
                new QualifiedObjectSqlQueryToken(
                    TokenFactory,
                    new ObjectNameSqlQueryToken(Column.Name));
    }
}
