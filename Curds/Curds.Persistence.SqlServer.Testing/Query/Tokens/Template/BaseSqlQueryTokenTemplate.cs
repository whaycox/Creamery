using Moq;

namespace Curds.Persistence.Query.Tokens.Template
{
    using Query.Abstraction;

    public abstract class BaseSqlQueryTokenTemplate
    {
        protected Mock<ISqlQueryFormatVisitor> MockFormatVisitor = new Mock<ISqlQueryFormatVisitor>();
    }
}
