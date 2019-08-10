using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.EFCore.Persistors.Tests
{
    using EFCore.Mock;
    using Mock;
    using Persistence.Mock;

    [TestClass]
    public class EntityPersistor : Template.EntityPersistor<IEntityPersistor, Context, IEntity>
    {
        protected override IEntityPersistor TestObject { get; } = new IEntityPersistor();

        protected override int SeededID => IEntity.Thirty.ID;
        protected override int NonSeededID => 7;
        protected override int[] ExpectedStartingIDs() => new int[]
        {
            IEntity.Ten.ID,
            IEntity.Twenty.ID,
            IEntity.Thirty.ID,
        };

        protected override int ExpectedStartingCount => IEntity.Samples.Length;
        protected override Context ExposedContext => TestObject.ExposedContext;
        protected override IEntity InsertEntity => new IEntity();

        protected override Context BuildContext() => new Context();

        protected override IEntity Modifier(IEntity entity)
        {
            Assert.AreEqual(IEntity.Thirty.Other, entity.Other);
            entity.Other = NonSeededID;
            return entity;
        }
        protected override void VerifyModifiedEntity(IEntity fetched)
        {
            Assert.AreEqual(SeededID, fetched.ID);
            Assert.AreEqual(NonSeededID, fetched.Other);
        }
    }

}
