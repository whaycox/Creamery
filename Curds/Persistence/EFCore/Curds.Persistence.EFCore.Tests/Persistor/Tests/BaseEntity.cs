using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using Curds.Persistence.Mock;
    using EFCore.Mock;

    [TestClass]
    public class BaseEntity : Template.BaseEntity<Mock.BaseEntity, Context, Persistence.Mock.BaseEntity>
    {
        private const int TestValue = 15;

        protected override Mock.BaseEntity TestObject { get; } = new Mock.BaseEntity();

        protected override int ExpectedStartingCount => Persistence.Mock.BaseEntity.Samples.Length;

        protected override Context RetrieveExposedContext => TestObject.ExposedContext;

        protected override Persistence.Mock.BaseEntity InsertEntity => new Persistence.Mock.BaseEntity() { MyValue = TestValue };

        protected override void VerifyInsertEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesAdded.Count);
            Assert.AreEqual(TestValue, TestObject.EntitiesAdded[0].Entity.MyValue);
        }
    }
}
