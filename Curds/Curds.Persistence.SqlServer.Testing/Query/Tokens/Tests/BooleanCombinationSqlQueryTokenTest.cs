using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Whey;

namespace Curds.Persistence.Query.Tokens.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Query.Domain;
    using Template;

    [TestClass]
    public class BooleanCombinationSqlQueryTokenTest : BaseSqlQueryTokenTemplate
    {
        private BooleanCombination TestCombination = BooleanCombination.Or;
        private List<ISqlQueryToken> TestElements = new List<ISqlQueryToken>();

        private BooleanCombinationSqlQueryToken TestObject = null;

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //TestObject = new BooleanCombinationSqlQueryToken(
            //    TestCombination,
            //    TestElements);
        }

        [DataTestMethod]
        [DataRow(BooleanCombination.And, SqlQueryKeyword.AND)]
        [DataRow(BooleanCombination.Or, SqlQueryKeyword.OR)]
        public void OperationTokenIsExpected(BooleanCombination testCombination, SqlQueryKeyword expectedKeyword)
        {
            throw new NotImplementedException();
            //TestObject = new BooleanCombinationSqlQueryToken(
            //    testCombination,
            //    TestElements);

            //KeywordSqlQueryToken operationToken = TestObject.Operation.VerifyIsActually<KeywordSqlQueryToken>();
            //Assert.AreEqual(expectedKeyword, operationToken.Keyword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCombinationThrows()
        {
            throw new NotImplementedException();
            //TestObject = new BooleanCombinationSqlQueryToken(
            //    (BooleanCombination)99,
            //    TestElements);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(16)]
        [DataRow(20)]
        public void ElementsArePopulatedFromConstructor(int elementsToAdd)
        {
            throw new NotImplementedException();
            //for (int i = 0; i < elementsToAdd; i++)
            //    TestElements.Add(Mock.Of<ISqlQueryToken>());
            //TestObject = new BooleanCombinationSqlQueryToken(
            //    TestCombination,
            //    TestElements);

            //CollectionAssert.AreEqual(TestElements, TestObject.Elements);
        }

        [TestMethod]
        public void VisitsFormatterProperly()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockFormatVisitor.Verify(visitor => visitor.VisitBooleanCombination(TestObject), Times.Once);
        }
    }
}
