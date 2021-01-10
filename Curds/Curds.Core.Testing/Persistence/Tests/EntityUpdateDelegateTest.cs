using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Persistence.Tests
{
    using Domain;

    [TestClass]
    public class EntityUpdateDelegateTest
    {
        private Action<TestSimpleEntity, int> TestUpdateDelegate = (entity, value) => entity.ID = value;
        private TestSimpleEntity TestEntity = new TestSimpleEntity();
        private int TestValue = 8;

        private EntityUpdateDelegate<TestSimpleEntity, int> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new EntityUpdateDelegate<TestSimpleEntity, int>(TestUpdateDelegate);
        }

        [TestMethod]
        public void UpdatePerformsDelegate()
        {
            TestObject.Update(TestEntity, TestValue);

            Assert.AreEqual(TestValue, TestEntity.ID);
        }
    }
}
