using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Query.Domain;
    using Template;

    [TestClass]
    public class InsertedIdentityColumnSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private string TestColumnName = nameof(TestColumnName);

        private Mock<ISqlColumn> MockColumn = new Mock<ISqlColumn>();

        private InsertedIdentityColumnSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockColumn
                .Setup(column => column.Name)
                .Returns(TestColumnName);

            TestObject = new InsertedIdentityColumnSqlQueryToken(MockColumn.Object);
        }

        [TestMethod]
        public void HasExpectedNameCount()
        {
            Assert.AreEqual(2, TestObject.Names.Count);
        }

        [TestMethod]
        public void FirstNameIsInserted()
        {
            Assert.AreEqual(nameof(SqlQueryKeyword.inserted), TestObject.Names[0].Name);
        }

        [TestMethod]
        public void SecondNameIsColumnName()
        {
            Assert.AreEqual(TestColumnName, TestObject.Names[1].Name);
        }

        [TestMethod]
        public void AcceptFormatVisitorInterspersesMultipleNamesWithSeparator()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<ObjectNameSqlQueryToken>(token => token.Name == nameof(SqlQueryKeyword.inserted))), Times.Once);
            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<ObjectNameSqlQueryToken>(token => token.Name == TestColumnName)), Times.Once);
            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<LiteralSqlQueryToken>(token => token.Literal == ".")), Times.Once);
        }
    }
}
