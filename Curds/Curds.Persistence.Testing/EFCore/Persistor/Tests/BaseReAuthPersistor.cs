using Curds.Domain.Persistence.EFCore;
using Curds.Domain.Persistence.EFCore.Persistor;
using Curds.Domain.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    [TestClass]
    public class BaseReAuthPersistor : BasePersistorTemplate<MockReAuthPersistor, ReAuth, MockSecureContext>
    {
        private MockProvider Provider = new MockProvider();

        protected override ReAuth Sample => MockReAuth.One;

        private MockReAuthPersistor _obj = null;
        protected override MockReAuthPersistor TestObject => _obj;

        private void VerifySame(ReAuth expected, ReAuth actual)
        {
            Assert.AreEqual(expected.DeviceIdentifier, actual.DeviceIdentifier);
            Assert.AreEqual(expected.Token, actual.Token);
            Assert.AreEqual(expected.Series, actual.Series);
            Assert.AreEqual(expected.UserID, actual.UserID);
        }

        [TestInitialize]
        public void Init()
        {
            Provider.Reset();
            _obj = new MockReAuthPersistor(Provider);
        }

        [TestMethod]
        public void CannotInsertSameSeries()
        {
            ReAuth first = Generic;
            TestObject.Insert(first).AwaitResult();
            ReAuth second = Generic;
            Assert.ThrowsException<ArgumentException>(() => TestObject.Insert(second).AwaitResult());
        }
        private ReAuth Generic
        {
            get
            {
                ReAuth toReturn = MockReAuth.One;
                toReturn.Series = nameof(ReAuth.Series);
                toReturn.Token = nameof(ReAuth.Token);
                return toReturn;
            }
        }

        [TestMethod]
        public void CanLookupBySeries()
        {
            ReAuth inserted = TestObject.Insert(Sample).AwaitResult();
            ReAuth fetched = TestObject.Lookup(inserted.Series).AwaitResult();
            VerifySame(inserted, fetched);
        }

        [TestMethod]
        public void CanLookupByUserID()
        {
            ReAuth first = TestObject.Insert(Sample).AwaitResult();
            ReAuth second = TestObject.Insert(Sample).AwaitResult();
            ReAuth third = TestObject.Insert(Sample).AwaitResult();
            List<ReAuth> fetched = TestObject.Lookup(MockUser.One.ID).AwaitResult();
            Assert.AreEqual(3, fetched.Count);
        }

        [TestMethod]
        public void CanUpdateToken()
        {
            ReAuth sample = Sample;
            TestObject.Insert(sample).AwaitResult();
            TestObject.Update(sample.Series, nameof(CanUpdateToken)).AwaitResult();
            sample.Token = nameof(CanUpdateToken);
            ReAuth fetched = TestObject.Lookup(sample.Series).AwaitResult();
            VerifySame(sample, fetched);
        }

        [TestMethod]
        public void CanDeleteBySeries()
        {
            ReAuth first = Sample;
            TestObject.Insert(first).AwaitResult();
            ReAuth second = Sample;
            TestObject.Insert(second).AwaitResult();
            TestObject.Delete(first.Series).AwaitResult();
            Assert.AreEqual(1, TestObject.Count.AwaitResult());
            Assert.IsNull(TestObject.Lookup(first.Series).AwaitResult());
        }

        [TestMethod]
        public void CanDeleteByUserID()
        {
            TestObject.Insert(Sample).AwaitResult();
            TestObject.Insert(Sample).AwaitResult();
            TestObject.Insert(Sample).AwaitResult();
            ReAuth otherUser = Sample;
            otherUser.UserID = MockUser.Three.ID;
            TestObject.Insert(otherUser).AwaitResult();
            TestObject.Delete(MockUser.One.ID).AwaitResult();

            Assert.AreEqual(1, TestObject.Count.AwaitResult());
            ReAuth fetched = TestObject.Lookup(otherUser.Series).AwaitResult();
            VerifySame(otherUser, fetched);
        }
    }
}
