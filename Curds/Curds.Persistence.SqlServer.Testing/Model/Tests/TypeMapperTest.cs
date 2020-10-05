using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

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
        private Type TestEntityType = typeof(OtherEntity);

        private TypeMapper TestObject = new TypeMapper();

        [TestMethod]
        public void MapsTableTypes()
        {
            List<Type> actual = TestObject.EntityTypes<ITestDataModel>().ToList();

            Assert.AreEqual(6, actual.Count());
            Type testEntity = actual[0];
            Assert.AreEqual(typeof(TestEntity), testEntity);
            Type otherEntity = actual[1];
            Assert.AreEqual(typeof(OtherEntity), otherEntity);
            Type enumEntity = actual[2];
            Assert.AreEqual(typeof(TestEnumEntity), enumEntity);
            Type token = actual[3];
            Assert.AreEqual(typeof(GenericToken), token);
            Type parent = actual[4];
            Assert.AreEqual(typeof(Parent), parent);
            Type child = actual[5];
            Assert.AreEqual(typeof(Child), child);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongEntitiesThrowMappingEntityTypes()
        {
            TestObject.EntityTypes<IWrongEntityModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void WrongPropertiesThrowMappingEntityTypes()
        {
            TestObject.EntityTypes<IWrongPropertyModel>();
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void NonInterfaceModelThrowsMappingEntityTypes()
        {
            TestObject.EntityTypes<NotAnInterfaceModel>();
        }

        [TestMethod]
        public void ValuesMapsOnlyReadWriteProperties()
        {
            List<PropertyInfo> expected = TestEntityType
                .GetProperties()
                .Where(property => property.Name != nameof(OtherEntity.Keys))
                .ToList();

            IEnumerable<PropertyInfo> actual = TestObject.ValueTypes(TestEntityType);

            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void ValuesAreInAlphabeticalOrder()
        {
            List<PropertyInfo> expected = TestEntityType
                .GetProperties()
                .Where(property => property.Name != nameof(OtherEntity.Keys))
                .OrderBy(property => property.Name)
                .ToList();

            IEnumerable<PropertyInfo> actual = TestObject.ValueTypes(TestEntityType);

            CollectionAssert.AreEqual(expected, actual.ToList());
        }
    }
}
