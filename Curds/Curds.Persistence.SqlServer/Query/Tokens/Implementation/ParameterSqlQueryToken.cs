using System;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    public class ParameterSqlQueryToken : LiteralSqlQueryToken
    {
        public override string Literal => $"@{Name}";
        public string Name { get; }
        public Type Type { get; }

        public ParameterSqlQueryToken(
            string parameterName, 
            Type parameterType)
        {
            Name = parameterName;
            Type = parameterType;
        }
    }
}
