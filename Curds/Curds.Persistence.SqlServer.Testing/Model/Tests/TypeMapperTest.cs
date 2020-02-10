using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Domain;
    using Abstraction;
    using Query.Domain;
    using Configuration.Abstraction;
    using Configuration.Domain;

    [TestClass]
    public class TypeMapperTest
    {
        private TypeMapper TestObject = new TypeMapper();

        [TestMethod]
        public void MapsTableTypes()
        {
            var actual = TestObject.TableTypes<ITestDataModel>();

            Assert.AreEqual(2, actual.Count());
            var first = actual.First();
            Assert.AreEqual(nameof(ITestDataModel.Test), first.tableName);
            Assert.AreEqual(typeof(TestEntity), first.tableType);
            var second = actual.Last();
            Assert.AreEqual(nameof(ITestDataModel.Other), second.tableName);
            Assert.AreEqual(typeof(OtherEntity), second.tableType);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonTablePropertiesThrowMappingTableTypes()
        {
            TestObject.TableTypes<IPlainEntityPropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongGenericPropertiesThrowMappingTableTypes()
        {
            TestObject.TableTypes<IWrongGenericTypePropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonInterfaceModelThrowsMappingTableTypes()
        {
            TestObject.TableTypes<NotAnInterfaceModel>();
        }
    }
}
