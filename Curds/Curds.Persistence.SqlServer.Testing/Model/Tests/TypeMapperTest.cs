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
            IEnumerable<Type> actual = TestObject.EntityTypes<ITestDataModel>();

            Assert.AreEqual(2, actual.Count());
            Type first = actual.First();
            Assert.AreEqual(typeof(TestEntity), first);
            Type second = actual.Last();
            Assert.AreEqual(typeof(OtherEntity), second);
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
