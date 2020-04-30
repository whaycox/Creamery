using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Persistence.Abstraction;
    using Persistence.Domain;

    [TestClass]
    public class TypeMapperTest
    {
        private TypeMapper TestObject = new TypeMapper();

        [TestInitialize]
        public void Revisit() => Assert.Fail();

        [TestMethod]
        public void MapsTableTypes()
        {
            var actual = TestObject.EntityTypes<ITestDataModel>();

            Assert.AreEqual(2, actual.Count());
            var first = actual.First();
            Assert.AreEqual(typeof(TestEntity), first);
            var second = actual.Last();
            Assert.AreEqual(typeof(OtherEntity), second);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonTablePropertiesThrowMappingTableTypes()
        {
            TestObject.EntityTypes<IPlainEntityPropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongGenericPropertiesThrowMappingTableTypes()
        {
            TestObject.EntityTypes<IWrongGenericTypePropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonInterfaceModelThrowsMappingTableTypes()
        {
            TestObject.EntityTypes<NotAnInterfaceModel>();
        }
    }
}
