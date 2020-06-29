using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class SetValuesSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private Mock<ISqlQueryToken> MockTokenOne = new Mock<ISqlQueryToken>();
        private Mock<ISqlQueryToken> MockTokenTwo = new Mock<ISqlQueryToken>();

        private SetValuesSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SetValuesSqlQueryToken(new[]
            {
                MockTokenOne.Object,
                MockTokenTwo.Object
            });
        }

        [TestMethod]
        public void SetValueTokensComeFromConstructor()
        {
            Assert.AreEqual(2, TestObject.SetValueTokens.Count);
            Assert.AreSame(MockTokenOne.Object, TestObject.SetValueTokens[0]);
            Assert.AreSame(MockTokenTwo.Object, TestObject.SetValueTokens[1]);
        }

        [TestMethod]
        public void AcceptFormatVisitorVisitsSetValues()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitSetValues(TestObject), Times.Once);
        }
    }
}
