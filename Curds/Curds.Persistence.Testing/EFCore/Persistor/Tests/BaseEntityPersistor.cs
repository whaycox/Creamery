using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence.EFCore.Persistor;
using Curds.Domain.Persistence;
using Curds.Domain.Persistence.EFCore;
using System.Threading;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    [TestClass]
    public class BaseEntityPersistor : BaseEntityPersistorTemplate<MockNameValueEntityPersistor, MockNameValueEntity, MockSecureContext>
    {
        private MockProvider Provider = new MockProvider();

        private MockNameValueEntityPersistor _obj = null;
        protected override MockNameValueEntityPersistor TestObject => _obj;
        
        protected override MockNameValueEntity Sample => MockNameValueEntity.Sample;

        protected override Func<MockNameValueEntity, MockNameValueEntity> Modifier => Modify;
        private MockNameValueEntity Modify(MockNameValueEntity sample)
        {
            sample.Name = nameof(Modify);
            sample.Value = nameof(Modifier);
            return sample;
        }

        [TestInitialize]
        public void Init()
        {
            Provider.Reset();
            _obj = new MockNameValueEntityPersistor(Provider);
        }

        [TestMethod]
        public void EFPersistorFiresAddedEvent()
        {
            Assert.AreEqual(0, TestObject.AddedEvents.Count);
            var inserted = TestObject.Insert(Sample).AwaitResult();
            Assert.AreEqual(1, TestObject.AddedEvents.Count);
            Assert.AreEqual(inserted, TestObject.AddedEvents[0].Entity);
        }

        [TestMethod]
        public void EFPersistorFiresUpdatedEvent()
        {
            var inserted = TestObject.Insert(Sample).AwaitResult();
            Assert.AreEqual(0, TestObject.UpdatedEvents.Count);
            TestObject.Update(inserted.ID, Modifier).AwaitResult();
            Assert.AreEqual(1, TestObject.UpdatedEvents.Count);
            Assert.AreEqual(Modify(inserted), TestObject.UpdatedEvents[0].Entity);
        }

        [TestMethod]
        public void EFPersistorFiresRemovedEvent()
        {
            var inserted = TestObject.Insert(Sample).AwaitResult();
            Assert.AreEqual(0, TestObject.RemovedEvents.Count);
            TestObject.Delete(inserted.ID).AwaitResult();
            Assert.AreEqual(1, TestObject.RemovedEvents.Count);
            Assert.AreEqual(inserted, TestObject.RemovedEvents[0].Entity);
        }
    }
}
