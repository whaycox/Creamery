using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence.EFCore.Persistor;
using Curds.Domain.Security;
using Curds.Domain.Persistence.EFCore;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    [TestClass]
    public class BaseSessionPersistor : BasePersistorTemplate<MockSessionPersistor, Session, MockSecureContext>
    {
        private MockProvider Provider = new MockProvider();

        protected override Session Sample => MockSession.Three;

        private MockSessionPersistor _obj = null;
        protected override MockSessionPersistor TestObject => _obj;

        private void VerifySame(Session expected, Session actual)
        {
            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.Series, actual.Series);
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Expiration, actual.Expiration);
        }

        private Session GenericIdentifier
        {
            get
            {
                Session toReturn = MockSession.Three;
                toReturn.Identifier = nameof(Session.Identifier);
                return toReturn;
            }
        }
        private Session GenericSeries
        {
            get
            {
                Session toReturn = MockSession.Three;
                toReturn.Series = nameof(Session.Series);
                return toReturn;
            }
        }

        [TestInitialize]
        public void Init()
        {
            Provider.Reset();
            _obj = new MockSessionPersistor(Provider);
        }

        [TestMethod]
        public void CannotInsertSameIdentifier()
        {
            Session first = GenericIdentifier;
            TestObject.Insert(first).AwaitResult();
            Session second = GenericIdentifier;
            Assert.ThrowsException<ArgumentException>(() => TestObject.Insert(second).AwaitResult());
        }

        [TestMethod]
        public void CanLookupByIdentifier()
        {
            Session inserted = TestObject.Insert(Sample).AwaitResult();
            Session fetched = TestObject.Lookup(inserted.Identifier).AwaitResult();
            VerifySame(inserted, fetched);
        }

        [TestMethod]
        public void CanUpdateExpiration()
        {
            Session sample = Sample;
            TestObject.Insert(sample).AwaitResult();
            TestObject.Update(sample.Identifier, Time.Fetch.AddHours(1)).AwaitResult();
            sample.Expiration = Time.Fetch.AddHours(1);
            Session fetched = TestObject.Lookup(sample.Identifier).AwaitResult();
            VerifySame(sample, fetched);
        }

        [TestMethod]
        public void CanDeleteBySeries()
        {
            Session first = Sample;
            TestObject.Insert(first).AwaitResult();
            Session second = Sample;
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
            Session otherUser = Sample;
            otherUser.UserID = MockUser.Two.ID;
            TestObject.Insert(otherUser).AwaitResult();
            TestObject.Delete(MockUser.Three.ID).AwaitResult();

            Assert.AreEqual(1, TestObject.Count.AwaitResult());
            Session fetched = TestObject.Lookup(otherUser.Identifier).AwaitResult();
            VerifySame(otherUser, fetched);
        }

        [TestMethod]
        public void CanDeleteByExpiration()
        {
            Session first = Sample;
            first.Expiration = Time.Fetch.AddMinutes(1);
            TestObject.Insert(first).AwaitResult();
            Session second = Sample;
            second.Expiration = Time.Fetch.AddMinutes(2);
            TestObject.Insert(second).AwaitResult();
            Session third = Sample;
            third.Expiration = Time.Fetch.AddMinutes(3);
            TestObject.Insert(third).AwaitResult();

            TestObject.Delete(Time.Fetch.AddMinutes(2.5)).AwaitResult();

            Assert.AreEqual(1, TestObject.Count.AwaitResult());
            Session fetched = TestObject.Lookup(third.Identifier).AwaitResult();
            VerifySame(third, fetched);
        }
    }
}
