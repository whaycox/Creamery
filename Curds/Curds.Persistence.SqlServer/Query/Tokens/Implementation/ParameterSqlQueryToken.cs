namespace Curds.Persistence.Query.Tokens.Implementation
{
    public class ParameterSqlQueryToken : LiteralSqlQueryToken
    {
        public override string Literal => $"@{Name}";
        public string Name { get; }

        public ParameterSqlQueryToken(string parameterName)
        {
            Name = parameterName;
        }
    }
}
