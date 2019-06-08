using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using EFCore.Mock;

    [TestClass]
    public class Entity : Template.Entity<Mock.Entity, Context, Persistence.Mock.Entity>
    {
        protected override Mock.Entity TestObject { get; } = new Mock.Entity();

        protected override int SeededID => Persistence.Mock.Entity.Thirty.ID;
        protected override int NonSeededID => 7;
        protected override int[] ManySeededIDs() => new int[]
        {
            Persistence.Mock.Entity.Ten.ID,
            Persistence.Mock.Entity.Twenty.ID,
            Persistence.Mock.Entity.Thirty.ID,
        };

        protected override int ExpectedStartingCount => 3;

        protected override Context RetrieveExposedContext => TestObject.ExposedContext;

        protected override Persistence.Mock.Entity InsertEntity => new Persistence.Mock.Entity();

        protected override Persistence.Mock.Entity Modifier(Persistence.Mock.Entity entity)
        {
            Assert.AreEqual(0, entity.Other);
            entity.Other = NonSeededID;
            return entity;
        }

        protected override void VerifyModifiedEntity(Persistence.Mock.Entity fetched)
        {
            Assert.AreEqual(SeededID, fetched.ID);
            Assert.AreEqual(NonSeededID, fetched.Other);
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
        }
        protected override void VerifyUpdateEventFired()
        {
            Assert.AreEqual(1, TestObject.EntitiesUpdated.Count);
            Assert.AreEqual(SeededID, TestObject.EntitiesUpdated[0].Entity.ID);
            Assert.AreEqual(NonSeededID, TestObject.EntitiesUpdated[0].Entity.Other);
        }
    }
}
