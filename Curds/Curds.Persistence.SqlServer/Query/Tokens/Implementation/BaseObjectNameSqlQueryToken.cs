namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class BaseObjectNameSqlQueryToken : BaseSqlQueryToken
    {
        public bool UseAlias { get; set; }

        public BaseObjectNameSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
            : base(tokenFactory)
        { }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) =>
            GenerateNameToken().AcceptFormatVisitor(visitor);
        protected abstract ISqlQueryToken GenerateNameToken();
    }
}
