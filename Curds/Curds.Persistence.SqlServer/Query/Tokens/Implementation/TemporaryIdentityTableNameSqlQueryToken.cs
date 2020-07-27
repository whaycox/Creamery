namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TemporaryIdentityTableNameSqlQueryToken : ObjectNameSqlQueryToken
    {
        public string BaseTableName { get; }

        public TemporaryIdentityTableNameSqlQueryToken(ISqlTable table)
            : base(FormatName(table.Name))
        {
            BaseTableName = table.Name;
        }
        public static string FormatName(string baseTableName) => $"#{baseTableName}_Identities";
    }
}
