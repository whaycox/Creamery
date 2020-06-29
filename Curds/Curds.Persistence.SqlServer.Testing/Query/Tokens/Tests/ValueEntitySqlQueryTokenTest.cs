using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class ValueEntitySqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ParameterSqlQueryToken> TestValues = new List<ParameterSqlQueryToken>();

        private ValueEntitySqlQueryToken TestObject = null;

        [TestMethod]
        public void ValuesSuppliedInConstructor()
        {
            for (int i = 0; i < 5; i++)
                TestValues.Add(new ParameterSqlQueryToken(i.ToString(), typeof(string)));
            TestObject = new ValueEntitySqlQueryToken(TestValues);

            Assert.AreEqual(TestValues.Count, TestObject.Values.Count);
            for (int i = 0; i < TestValues.Count; i++)
                Assert.AreSame(TestValues[i], TestObject.Values[i]);
        }

        [TestMethod]
        public void VisitsFormatterAsValueEntity()
        {
            TestObject = new ValueEntitySqlQueryToken(TestValues);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitValueEntity(TestObject), Times.Once);
        }

    }
}
