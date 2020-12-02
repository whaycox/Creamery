using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Clone.Tests
{
    using Abstraction;
    using Domain;

    [TestClass]
    public class CloneIntegrationTest
    {
        private ServiceCollection TestServiceCollection = new ServiceCollection();
        private ServiceProvider TestServiceProvider = null;
        private PrimitiveEntity TestPrimitiveEntity = new PrimitiveEntity();
        private ComplexEntity TestComplexEntity = new ComplexEntity();

        private void RegisterServices()
        {
            TestServiceCollection.AddCurdsCore();
        }

        private void BuildServiceProvider()
        {
            TestServiceProvider = TestServiceCollection.BuildServiceProvider();
        }

        [TestInitialize]
        public void Init()
        {
            TestComplexEntity.TestPrimitiveEntity = TestPrimitiveEntity;

            RegisterServices();
            BuildServiceProvider();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestServiceProvider?.Dispose();
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
    }
}
