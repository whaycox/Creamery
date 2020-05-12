using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class ValueEntitiesSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private List<ValueEntitySqlQueryToken> TestEntities = new List<ValueEntitySqlQueryToken>();
        private List<ParameterSqlQueryToken> TestValues = new List<ParameterSqlQueryToken>();

        private ValueEntitiesSqlQueryToken TestObject = null;

        [TestMethod]
        public void EntitiesSuppliedInConstructor()
        {
            for (int i = 0; i < 5; i++)
                TestEntities.Add(new ValueEntitySqlQueryToken(TestValues));
            TestObject = new ValueEntitiesSqlQueryToken(TestEntities);

            Assert.AreEqual(TestEntities.Count, TestObject.Entities.Count);
            for (int i = 0; i < TestEntities.Count; i++)
                Assert.AreSame(TestEntities[i], TestObject.Entities[i]);
        }

        [TestMethod]
        public void VisitsFormatterAsValueEntities()
        {
            TestObject = new ValueEntitiesSqlQueryToken(TestEntities);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitValueEntities(TestObject), Times.Once);
        }
    }
}
