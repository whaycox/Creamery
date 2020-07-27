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
            SetupTokenFactoryForMockToken(factory => factory.GroupedList(It.IsAny<IEnumerable<ISqlQueryToken>>(), It.IsAny<bool>()));

            BuildTestObject(TestCombination);
        }
        private void BuildTestObject(BooleanCombination testCombination)
        {
            TestObject = new BooleanCombinationSqlQueryToken(
                MockTokenFactory.Object,
                testCombination,
                TestElements);
        }

        [DataTestMethod]
        [DataRow(BooleanCombination.And, SqlQueryKeyword.AND)]
        [DataRow(BooleanCombination.Or, SqlQueryKeyword.OR)]
        public void BuildingTokenGeneratesExpectedKeyword(BooleanCombination testCombination, SqlQueryKeyword expectedKeyword)
        {
            MockTokenFactory.Reset();

            BuildTestObject(testCombination);

            MockTokenFactory.Verify(factory => factory.Keyword(expectedKeyword), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCombinationThrows()
        {
            BuildTestObject((BooleanCombination)99);
        }

        private void AddNElements(int elementsToAdd)
        {
            for (int i = 0; i < elementsToAdd; i++)
                TestElements.Add(Mock.Of<ISqlQueryToken>());
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(16)]
        [DataRow(20)]
        public void ElementsArePopulatedFromConstructor(int elementsToAdd)
        {
            AddNElements(elementsToAdd);
            BuildTestObject(TestCombination);

            CollectionAssert.AreEqual(TestElements, TestObject.Elements);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(16)]
        [DataRow(20)]
        public void AcceptFormatVisitorGeneratesExpectedToken(int elementsToAdd)
        {
            AddNElements(elementsToAdd);
            BuildTestObject(TestCombination);
            SetupTokenFactoryForMockToken(factory => factory.Phrase(It.IsAny<ISqlQueryToken[]>()));
            List<ISqlQueryToken> expectedPhrases = new List<ISqlQueryToken>();
            for (int i = 0; i < elementsToAdd; i++)
                expectedPhrases.Add(MockToken.Object);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.GroupedList(expectedPhrases, false), Times.Once);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(16)]
        [DataRow(20)]
        public void AcceptFormatVisitorGeneratesExpectedPhrases(int elementsToAdd)
        {
            AddNElements(elementsToAdd);
            BuildTestObject(TestCombination);

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(
                It.Is<ConstantSqlQueryToken>(token => token.Literal == "("),
                It.IsAny<ISqlQueryToken>(),
                It.Is<ConstantSqlQueryToken>(token => token.Literal == ")")), Times.Exactly(elementsToAdd));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(16)]
        [DataRow(20)]
        public void SubsequentElementsAreWrappedWithOperation(int elementsToAdd)
        {
            ISqlQueryToken keywordToken = MockTokenFactory.SetupMock(factory => factory.Keyword(It.IsAny<SqlQueryKeyword>()));
            AddNElements(elementsToAdd);
            BuildTestObject(TestCombination);
            SetupTokenFactoryForMockToken(factory => factory.Phrase(
                It.Is<ConstantSqlQueryToken>(token => token.Literal == "("),
                It.IsAny<ISqlQueryToken>(),
                It.Is<ConstantSqlQueryToken>(token => token.Literal == ")")));

            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockTokenFactory.Verify(factory => factory.Phrase(keywordToken, MockToken.Object), Times.Exactly(elementsToAdd - 1));
        }

        [TestMethod]
        public void AcceptFormatVisitorPassesToGeneratedToken()
        {
            TestObject.AcceptFormatVisitor(MockFormatVisitor.Object);

            MockToken.Verify(token => token.AcceptFormatVisitor(MockFormatVisitor.Object), Times.Once);
        }
    }
}
