using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Whey;

namespace Curds.Persistence.Query.Tests
{
    using Abstraction;
    using Implementation;
    using Persistence.Abstraction;

    [TestClass]
    public class SqlQueryExpressionVisitorFactoryTest
    {
        private Mock<ISqlQueryContext<ITestDataModel>> MockQueryContext = new Mock<ISqlQueryContext<ITestDataModel>>();

        private SqlQueryExpressionVisitorFactory TestObject = new SqlQueryExpressionVisitorFactory();

        [TestMethod]
        public void TableVisitorIsExpectedType()
        {
            ISqlTableVisitor<ITestDataModel> actual = TestObject.TableVisitor(MockQueryContext.Object);

            Assert.IsInstanceOfType(actual, typeof(TableExpressionVisitor<ITestDataModel>));
        }

        [TestMethod]
        public void TableVisitorPassesQueryContext()
        {
            ISqlTableVisitor<ITestDataModel> actual = TestObject.TableVisitor(MockQueryContext.Object);

            TableExpressionVisitor<ITestDataModel> actualVisitor = actual.VerifyIsActually<TableExpressionVisitor<ITestDataModel>>();
            Assert.AreSame(MockQueryContext.Object, actualVisitor.Context);
        }

        [TestMethod]
        public void TokenVisitorIsExpectedType()
        {
            ISqlQueryTokenVisitor<ITestDataModel> actual = TestObject.TokenVisitor(MockQueryContext.Object);

            Assert.IsInstanceOfType(actual, typeof(TokenExpressionVisitor<ITestDataModel>));
        }

        [TestMethod]
        public void TokenVisitorPassesQueryContext()
        {
            ISqlQueryTokenVisitor<ITestDataModel> actual = TestObject.TokenVisitor(MockQueryContext.Object);

            TokenExpressionVisitor<ITestDataModel> actualVisitor = actual.VerifyIsActually<TokenExpressionVisitor<ITestDataModel>>();
            Assert.AreSame(MockQueryContext.Object, actualVisitor.Context);
        }
    }
}
