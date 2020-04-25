namespace Curds.Persistence.Query.Tokens.Implementation
{
    public class ObjectNameSqlQueryToken : LiteralSqlQueryToken
    {
        public override string Literal => $"[{Name}]";
        public string Name { get; }

        public ObjectNameSqlQueryToken(string name)
        {
            Name = name;
        }
    }
}
