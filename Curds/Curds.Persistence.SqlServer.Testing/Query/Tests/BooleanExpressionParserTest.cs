using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Domain;
    using Persistence.Abstraction;

    [TestClass]
    public class BooleanExpressionParserTest
    {
        private object[] TestKeyArray = new object[0];

        //private void VerifyKeyEqualityParsedCorrectly(ParsedBooleanExpression actual)
        //{
        //    Assert.AreEqual(SqlBooleanOperation.Equals, actual.Operation);
        //    Assert.AreEqual(nameof(TestEntity.Keys), actual.PropertyName);
        //    Assert.AreSame(TestKeyArray, actual.Value);
        //}

        [TestMethod]
        public void CanParseKeyEqualityFromProperty()
        {
            throw new NotImplementedException();
            //ParsedBooleanExpression actual = TestObject.Parse<TestEntity>(entity => entity.Keys == TestKeyArray);

            //VerifyKeyEqualityParsedCorrectly(actual);
        }

        [TestMethod]
        public void CanParseKeyEqualityFromMethodArgument() => CanParseKeyEqualityFromMethodArgumentHelper(TestKeyArray);
        private void CanParseKeyEqualityFromMethodArgumentHelper(object[] testKeys)
        {
            throw new NotImplementedException();
            //ParsedBooleanExpression actual = TestObject.Parse<TestEntity>(entity => entity.Keys == testKeys);

            //VerifyKeyEqualityParsedCorrectly(actual);
        }

        [TestMethod]
        public void CanParseKeyEqualityFromVariable()
        {
            throw new NotImplementedException();
            //object[] testKeys = TestKeyArray;

            //ParsedBooleanExpression actual = TestObject.Parse<TestEntity>(entity => entity.Keys == testKeys);

            //VerifyKeyEqualityParsedCorrectly(actual);
        }

        [TestMethod]
        public void OrderingOnKeyEqualityDoesntMatter()
        {
            throw new NotImplementedException();
            //ParsedBooleanExpression actual = TestObject.Parse<TestEntity>(entity => TestKeyArray == entity.Keys);

            //VerifyKeyEqualityParsedCorrectly(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void ThrowsWhenNoParameterUsed() => ThrowsWhenNoParameterUsedInternal(TestKeyArray);
        private void ThrowsWhenNoParameterUsedInternal(object[] testKeys) => throw new NotImplementedException();
            //TestObject.Parse<TestEntity>(entity => testKeys == TestKeyArray);

        [TestMethod]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void ThrowsWhenNonParameterConverted()
        {
            throw new NotImplementedException();
            //TestEntity testEntity = new TestEntity();

            //TestObject.Parse<TestEntity>(entity => ((IEntity)testEntity).Keys == TestKeyArray);
        }
    }
}
