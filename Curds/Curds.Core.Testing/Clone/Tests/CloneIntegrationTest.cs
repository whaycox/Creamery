using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Clone.Tests
{
    using Abstraction;
    using Curds.Template;
    using Domain;

    [TestClass]
    public class CloneIntegrationTest : IntegrationTemplate
    {
        private PrimitiveEntity TestPrimitiveEntity = new PrimitiveEntity();
        private ComplexEntity TestComplexEntity = new ComplexEntity();

        [TestInitialize]
        public void Init()
        {
            TestComplexEntity.TestPrimitiveEntity = TestPrimitiveEntity;

            RegisterServices();
            BuildServiceProvider();
        }

        private void VerifyPrimitiveEntityWasCloned(PrimitiveEntity actual)
        {
            Assert.AreNotSame(TestPrimitiveEntity, actual);
            Assert.AreEqual(TestPrimitiveEntity.TestByte, actual.TestByte);
            Assert.AreEqual(TestPrimitiveEntity.TestShort, actual.TestShort);
            Assert.AreEqual(TestPrimitiveEntity.TestInt, actual.TestInt);
            Assert.AreEqual(TestPrimitiveEntity.TestLong, actual.TestLong);
            Assert.AreEqual(TestPrimitiveEntity.TestDateTime, actual.TestDateTime);
            Assert.AreEqual(TestPrimitiveEntity.TestDateTimeOffset, actual.TestDateTimeOffset);
            Assert.AreEqual(TestPrimitiveEntity.TestString, actual.TestString);
        }

        [TestMethod]
        public void CanCloneEntityWithPrimitives()
        {
            ICloneFactory testObject = TestServiceProvider.GetRequiredService<ICloneFactory>();

            PrimitiveEntity actual = testObject.Clone(TestPrimitiveEntity);

            VerifyPrimitiveEntityWasCloned(actual);
        }

        [TestMethod]
        public void CanCloneEntityWithComplexTypes()
        {
            ICloneFactory testObject = TestServiceProvider.GetRequiredService<ICloneFactory>();

            ComplexEntity actual = testObject.Clone(TestComplexEntity);

            Assert.AreNotSame(TestComplexEntity, actual);
            Assert.AreEqual(TestComplexEntity.TestInt, actual.TestInt);
            Assert.AreNotSame(TestComplexEntity.TestPrimitiveEntity, actual.TestPrimitiveEntity);
            VerifyPrimitiveEntityWasCloned(actual.TestPrimitiveEntity);
        }

        [DataTestMethod]
        [DataRow(10)]
        [DataRow(50)]
        [DataRow(100)]
        public void CanCloneEntityWithCollections(int testEntitiesInCollection)
        {
            CollectionEntity testEntity = new CollectionEntity(testEntitiesInCollection);
            ICloneFactory testObject = TestServiceProvider.GetRequiredService<ICloneFactory>();

            CollectionEntity actual = testObject.Clone(testEntity);

            Assert.AreNotSame(testEntity, actual);
            Assert.AreEqual(testEntitiesInCollection, actual.IntArray.Length);
            Assert.AreEqual(testEntitiesInCollection, actual.PrimitiveEntityArray.Length);
            Assert.AreEqual(testEntitiesInCollection, actual.ComplexEntityArray.Length);
            Assert.AreEqual(testEntitiesInCollection, actual.LongList.Count);
            Assert.AreEqual(testEntitiesInCollection, actual.PrimitiveEntityList.Count);
            Assert.AreEqual(testEntitiesInCollection, actual.ComplexEntityList.Count);
            for (int i = 0; i < testEntitiesInCollection; i++)
                VerifyIndexWasCloned(testEntity, actual, i);
        }
        private void VerifyIndexWasCloned(CollectionEntity expected, CollectionEntity actual, int entityIndex)
        {
            Assert.AreEqual(expected.IntArray[entityIndex], actual.IntArray[entityIndex]);
            Assert.AreNotSame(expected.PrimitiveEntityArray[entityIndex], actual.PrimitiveEntityArray[entityIndex]);
            Assert.AreEqual(expected.PrimitiveEntityArray[entityIndex], actual.PrimitiveEntityArray[entityIndex]);
            Assert.AreNotSame(expected.ComplexEntityArray[entityIndex], actual.ComplexEntityArray[entityIndex]);
            Assert.AreEqual(expected.ComplexEntityArray[entityIndex], actual.ComplexEntityArray[entityIndex]);

            Assert.AreEqual(expected.LongList[entityIndex], actual.LongList[entityIndex]);
            Assert.AreNotSame(expected.PrimitiveEntityList[entityIndex], actual.PrimitiveEntityList[entityIndex]);
            Assert.AreEqual(expected.PrimitiveEntityList[entityIndex], actual.PrimitiveEntityList[entityIndex]);
            Assert.AreNotSame(expected.ComplexEntityList[entityIndex], actual.ComplexEntityList[entityIndex]);
            Assert.AreEqual(expected.ComplexEntityList[entityIndex], actual.ComplexEntityList[entityIndex]);
        }
    }
}
