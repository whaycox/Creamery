using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class QualifiedObjectSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private string TestNameOne = nameof(TestNameOne);
        private string TestNameTwo = nameof(TestNameTwo);
        private List<string> TestNames = new List<string>();

        private QualifiedObjectSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestNames.Add(TestNameOne);
            TestNames.Add(TestNameTwo);

            SetupTokenFactoryForMockToken(factory => factory.ObjectName(It.IsAny<string>()));

            BuildTestObject();
        }
        private void BuildTestObject()
        {
            TestObject = new QualifiedObjectSqlQueryToken(
                MockTokenFactory.Object,
                TestNames);
        }

        [TestMethod]
        public void NamesComeFromConstructor()
        {
            CollectionAssert.AreEqual(TestNames, TestObject.Names);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        public void EmptyNamesAreDisregarded(string testName)
        {
            TestNames.Add(testName);
            BuildTestObject();

            CollectionAssert.AreEqual(new[] { TestNameOne, TestNameTwo }, TestObject.Names);
        }

        [TestMethod]
        public void AcceptFormatVisitorForwardsSingleName()
        {
            TestNames.Clear();
            TestNames.Add(TestNameOne);
            BuildTestObject();

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorInterspersesMultipleNamesWithSeparator()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Exactly(2));
            MockFormatVisitor.Verify(visitor => visitor.VisitLiteral(It.Is<ConstantSqlQueryToken>(token => token.Literal == ".")), Times.Once);
        }

        [TestMethod]
        public void AcceptFormatVisitorGeneratesTokenForEachName()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.ObjectName(TestNameOne), Times.Once);
            MockTokenFactory.Verify(factory => factory.ObjectName(TestNameTwo), Times.Once);
        }
    }
}
