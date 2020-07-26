using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Query.Domain;
    using Template;
    using Values.Domain;

    [TestClass]
    public class ValueEntitySqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private ValueEntity TestValueEntity = new ValueEntity();

        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();

        private ValueEntitySqlQueryToken TestObject = null;

        private IntValue TestIntValue(int testInt) => new IntValue
        {
            Int = testInt,
            Name = testInt.ToString(),
        };

        private void BuildTestObject()
        {
            TestObject = new ValueEntitySqlQueryToken(
                MockTokenFactory.Object,
                MockParameterBuilder.Object,
                TestValueEntity);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void BuildsParameterForEachValue(int parameters)
        {
            for (int i = 0; i < parameters; i++)
                TestValueEntity.Values.Add(TestIntValue(i));

            BuildTestObject();

            for (int i = 0; i < parameters; i++)
                MockTokenFactory.Verify(factory => factory.Parameter(MockParameterBuilder.Object, i.ToString(), i), Times.Once);
        }

        [TestMethod]
        public void ValuesAreBuiltParameters()
        {
            ISqlQueryToken mockValueToken = Mock.Of<ISqlQueryToken>();
            MockTokenFactory
                .Setup(factory => factory.Parameter(MockParameterBuilder.Object, It.IsAny<string>(), It.IsAny<object>()))
                .Returns(mockValueToken);
            TestValueEntity.Values.Add(TestIntValue(1));

            BuildTestObject();

            CollectionAssert.AreEqual(new List<ISqlQueryToken> { mockValueToken }, TestObject.Values);
        }

        [TestMethod]
        public void VisitsFormatterAsValueEntity()
        {
            BuildTestObject();

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitValueEntity(TestObject), Times.Once);
        }
    }
}
