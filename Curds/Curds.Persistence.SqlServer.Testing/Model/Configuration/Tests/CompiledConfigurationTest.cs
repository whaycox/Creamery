using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Tests
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class CompiledConfigurationTest
    {
        private Type TestEntityType = typeof(TestEntity);
        private CompiledColumnConfiguration<ITestDataModel> TestColumnConfiguration = new CompiledColumnConfiguration<ITestDataModel>(nameof(TestColumnConfiguration));

        private CompiledConfiguration<ITestDataModel> TestObject = null;
        private IEntityConfiguration InterfaceTestObject => TestObject;

        [TestInitialize]
        public void Init()
        {
            TestObject = new CompiledConfiguration<ITestDataModel>(TestEntityType);
        }

        [TestMethod]
        public void TypesAreExpected()
        {
            Assert.AreEqual(typeof(ITestDataModel), TestObject.ModelType);
            Assert.AreEqual(TestEntityType, TestObject.EntityType);
        }

        [TestMethod]
        public void InterfaceKeysComeFromCollection()
        {
            TestObject.Keys.Add(nameof(InterfaceKeysComeFromCollection));

            IList<string> actual = InterfaceTestObject.Keys;

            Assert.AreSame(TestObject.Keys, actual);
        }

        [TestMethod]
        public void InterfaceColumnsAreDictionaryValues()
        {
            TestObject.Columns.Add(nameof(InterfaceColumnsAreDictionaryValues), TestColumnConfiguration);

            IList<IColumnConfiguration> actual = InterfaceTestObject.Columns;

            Assert.AreEqual(TestObject.Columns.Count, actual.Count);
            Assert.AreSame(TestObject.Columns[nameof(InterfaceColumnsAreDictionaryValues)], actual[0]);
        }
    }
}
