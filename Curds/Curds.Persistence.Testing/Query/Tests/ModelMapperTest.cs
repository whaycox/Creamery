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
    using Persistence.Abstraction;
    using Domain;
    using Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class ModelMapperTest
    {
        private TestEntity TestEntity = new TestEntity();
        private int TestID = 176;
        private string TestName = nameof(TestName);

        private ModelMapper TestObject = new ModelMapper();

        [TestInitialize]
        public void Init()
        {
            TestEntity.ID = TestID;
            TestEntity.Name = TestName;
        }

        [TestMethod]
        public void MapsTablesByNameCorrectly()
        {
            Dictionary<string, Table> actual = TestObject.MapTablesByName<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            Table actualTable = actual[nameof(ITestDataModel.Test)];
            Assert.AreEqual(string.Empty, actualTable.Schema);
            Assert.AreEqual(nameof(ITestDataModel.Test), actualTable.Name);
        }

        [TestMethod]
        public void MapsMultipleTablesByNameCorrectly()
        {
            Dictionary<string, Table> actual = TestObject.MapTablesByName<IOtherDataModel>();

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.ContainsKey(nameof(IOtherDataModel.TestEntities)));
            Assert.IsTrue(actual.ContainsKey(nameof(IOtherDataModel.Others)));
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void PlainEntityPropertiesThrowOnMapTablesByName()
        {
            TestObject.MapTablesByName<IPlainEntityPropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongGenericTypePropertiesThrowOnMapTablesByName()
        {
            TestObject.MapTablesByName<IWrongGenericTypePropertyModel>();
        }


        [TestMethod]
        public void MapsTablesByTypeCorrectly()
        {
            Dictionary<Type, Table> actual = TestObject.MapTablesByType<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            Table actualTable = actual[typeof(TestEntity)];
            Assert.AreEqual(string.Empty, actualTable.Schema);
            Assert.AreEqual(nameof(ITestDataModel.Test), actualTable.Name);
        }

        [TestMethod]
        public void MapsMultipleTablesByTypeCorrectly()
        {
            Dictionary<Type, Table> actual = TestObject.MapTablesByType<IOtherDataModel>();

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.ContainsKey(typeof(TestEntity)));
            Assert.IsTrue(actual.ContainsKey(typeof(OtherEntity)));
        }

        [TestMethod]
        public void MapsValueEntitiesByTypeCorrectly()
        {
            Dictionary<Type, ValueEntityDelegate> actual = TestObject.MapValueEntityDelegates<ITestDataModel>();

            Assert.AreEqual(1, actual.Count);
            ValueEntityDelegate valueEntityDelegate = actual[typeof(TestEntity)];
            ValueEntity valueEntity = valueEntityDelegate(TestEntity);
            Assert.AreEqual(2, valueEntity.Values.Count);
            Assert.AreEqual(nameof(TestEntity.ID), valueEntity.Values[0].Name);
            Assert.AreEqual(TestID, valueEntity.Values[0].Content);
            Assert.AreEqual(nameof(TestEntity.Name), valueEntity.Values[1].Name);
            Assert.AreEqual(TestName, valueEntity.Values[1].Content);
        }

    }
}
