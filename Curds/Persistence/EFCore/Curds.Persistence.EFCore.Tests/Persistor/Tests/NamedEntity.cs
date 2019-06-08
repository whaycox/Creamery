using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using EFCore.Mock;

    [TestClass]
    public class NamedEntity : Template.Entity<Mock.NamedEntity, Context, Persistence.Mock.NamedEntity>
    {
        protected override int SeededID => Persistence.Mock.NamedEntity.Ten.ID;
        protected override int NonSeededID => 5;
        protected override int[] ManySeededIDs() => new int[]
        {
            Persistence.Mock.NamedEntity.Ten.ID,
            Persistence.Mock.NamedEntity.Twenty.ID,
            Persistence.Mock.NamedEntity.Thirty.ID,
        };

        protected override int ExpectedStartingCount => 3;

        protected override Context RetrieveExposedContext => TestObject.ExposedContext;

        protected override Persistence.Mock.NamedEntity InsertEntity => new Persistence.Mock.NamedEntity() { Name = nameof(NamedEntity) };

        protected override Mock.NamedEntity TestObject { get; } = new Mock.NamedEntity();

        protected override Persistence.Mock.NamedEntity Modifier(Persistence.Mock.NamedEntity entity)
        {
            entity.Name = nameof(Modifier);
            return entity;
        }

        protected override void VerifyModifiedEntity(Persistence.Mock.NamedEntity fetched)
        {
            Assert.AreEqual(SeededID, fetched.ID);
            Assert.AreEqual(nameof(Modifier), fetched.Name);
        }

        protected override void VerifyDeleteEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesRemoved.Count);
            Assert.AreEqual(SeededID, TestObject.EntitiesRemoved[0].Entity.ID);
            Assert.AreEqual(nameof(NamedEntity), TestObject.EntitiesRemoved[0].Entity.Name);
        }
        protected override void VerifyInsertEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesAdded.Count);
            Assert.AreNotEqual(0, TestObject.EntitiesAdded[0].Entity.ID);
            Assert.AreEqual(nameof(NamedEntity), TestObject.EntitiesAdded[0].Entity.Name);
        }
        protected override void VerifyUpdateEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesUpdated.Count);
            Assert.AreEqual(SeededID, TestObject.EntitiesUpdated[0].Entity.ID);
            Assert.AreEqual(nameof(Modifier), TestObject.EntitiesUpdated[0].Entity.Name);
        }
    }
}
