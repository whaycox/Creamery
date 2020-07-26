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

        protected override ISqlQueryToken RedirectedToken() =>
            UseAlias ?
                TokenFactory.QualifiedObject(
                    Column.Table.Alias,
                    Column.Name) :
                TokenFactory.QualifiedObject(
                    Column.Name);
    }
}
