namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Model.Abstraction;
    using Persistence.Abstraction;

    public class TemporaryIdentityTableNameSqlQueryToken : ObjectNameSqlQueryToken
    {
        public string BaseTableName { get; }

        public TemporaryIdentityTableNameSqlQueryToken(IEntityModel entityModel)
            : base(FormatName(entityModel.Table))
        {
            BaseTableName = entityModel.Table;
        }
        public static string FormatName(string baseTableName) => $"#{baseTableName}_Identities";
    }
}
