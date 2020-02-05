using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;

    [TestClass]
    public class ExpressionParserTest
    {
        private ExpressionParser TestObject = new ExpressionParser();

        [TestMethod]
        public void CanParseTestEntityID()
        {
            string actual = TestObject.ParseEntityValueSelection<TestEntity, int>(entity => entity.ID);

            Assert.AreEqual(nameof(TestEntity.ID), actual);
        }

        [TestMethod]
        public void CanParseTestEntityKeys()
        {
            string actual = TestObject.ParseEntityValueSelection<TestEntity, object[]>(entity => entity.Keys);

            Assert.AreEqual(nameof(TestEntity.Keys), actual);
        }

        [TestMethod]
        public void CanParseTestEntityName()
        {
            string actual = TestObject.ParseEntityValueSelection<TestEntity, string>(entity => entity.Name);

            Assert.AreEqual(nameof(TestEntity.Name), actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParsingEntityValueSelectionWithDelegateThrows()
        {
            TestObject.ParseEntityValueSelection<TestEntity, int>(entity => InvalidSelectionDelegate());
        }
        private int InvalidSelectionDelegate() => throw new NotImplementedException();

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParsingEntityValueSelectionWithConstantThrows()
        {
            TestObject.ParseEntityValueSelection<TestEntity, int>(entity => 5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParsingEntityValueSelectionWithoutParameterThrows()
        {
            TestEntity testEntity = new TestEntity();

            TestObject.ParseEntityValueSelection<TestEntity, int>(entity => testEntity.ID);
        }

    }
}
