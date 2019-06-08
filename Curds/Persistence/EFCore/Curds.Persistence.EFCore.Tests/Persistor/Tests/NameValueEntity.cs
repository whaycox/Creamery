using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using EFCore.Mock;

    [TestClass]
    public class NameValueEntity : Template.Entity<Mock.NameValueEntity, Context, Persistence.Mock.NameValueEntity>
    {
        protected override int SeededID => Persistence.Mock.NameValueEntity.Twenty.ID;
        protected override int NonSeededID => 15;
        protected override int[] ManySeededIDs() => new int[]
        {
            Persistence.Mock.NameValueEntity.Ten.ID,
            Persistence.Mock.NameValueEntity.Twenty.ID,
            Persistence.Mock.NameValueEntity.Thirty.ID,
        };

        protected override int ExpectedStartingCount => 3;

        protected override Context RetrieveExposedContext => TestObject.ExposedContext;

        protected override Persistence.Mock.NameValueEntity InsertEntity => new Persistence.Mock.NameValueEntity() { Name = nameof(NameValueEntity), Value = nameof(InsertEntity) };

        protected override Mock.NameValueEntity TestObject { get; } = new Mock.NameValueEntity();

        protected override Persistence.Mock.NameValueEntity Modifier(Persistence.Mock.NameValueEntity entity)
        {
            entity.Name = nameof(Modifier);
            entity.Value = nameof(Modifier);
            return entity;
        }

        protected override void VerifyModifiedEntity(Persistence.Mock.NameValueEntity fetched)
        {
            Assert.AreEqual(SeededID, fetched.ID);
            Assert.AreEqual(nameof(Modifier), fetched.Name);
            Assert.AreEqual(nameof(Modifier), fetched.Value);
        }

        protected override void VerifyDeleteEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesRemoved.Count);
            Assert.AreEqual(SeededID, TestObject.EntitiesRemoved[0].Entity.ID);
        }
        protected override void VerifyInsertEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesAdded.Count);
            Assert.AreNotEqual(0, TestObject.EntitiesAdded[0].Entity.ID);
            Assert.AreEqual(nameof(NameValueEntity), TestObject.EntitiesAdded[0].Entity.Name);
            Assert.AreEqual(nameof(InsertEntity), TestObject.EntitiesAdded[0].Entity.Value);
        }
        protected override void VerifyUpdateEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesUpdated.Count);
            Assert.AreEqual(SeededID, TestObject.EntitiesUpdated[0].Entity.ID);
            Assert.AreEqual(nameof(Modifier), TestObject.EntitiesUpdated[0].Entity.Name);
            Assert.AreEqual(nameof(Modifier), TestObject.EntitiesUpdated[0].Entity.Value);
        }
    }
}
