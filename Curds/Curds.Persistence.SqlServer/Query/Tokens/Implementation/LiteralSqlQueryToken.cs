using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class LiteralSqlQueryToken : BaseSqlQueryToken
    {
        public abstract string Literal { get; }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitLiteral(this);
    }
}
