using Moq;

namespace Curds.Persistence.Query.Tokens.Template
{
    using Implementation;

    public abstract class LiteralSqlQueryTokenTemplate : BaseSqlQueryTokenTemplate
    {
        protected void VerifyTokenAcceptsLiteralToken(LiteralSqlQueryToken testObject)
        {
            testObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(testObject), Times.Once);
        }
    }
}
