namespace Curds.Persistence.Query.Tokens.Implementation
{
    public class ConstantSqlQueryToken : LiteralSqlQueryToken
    {
        private string _constant;
        public override string Literal => _constant;

        public ConstantSqlQueryToken(string constant)
        {
            _constant = constant;
        }
    }
}
