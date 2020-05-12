namespace Curds.Persistence.Query.Tokens.Implementation
{
    public class ConstantSqlQueryToken : LiteralSqlQueryToken
    {
        private string Constant { get; }
        public override string Literal => Constant;

        public ConstantSqlQueryToken(string constant)
        {
            Constant = constant;
        }
    }
}
