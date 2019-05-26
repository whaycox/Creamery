using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Security.Tests
{
    [TestClass]
    public class Session : Template.SecureObject
    {
        [TestMethod]
        public void SessionIDIsBase64()
        {
            VerifyBase64(Domain.Session.NewSessionID, Domain.Session.SessionIdentifierLengthInBytes);
        }

        [TestMethod]
        public void DifferentSessionIDEachTime()
        {
            Assert.AreNotEqual(Domain.Session.NewSessionID, Domain.Session.NewSessionID);
        }

        [TestMethod]
        public void CanIncrementExpiration()
        {
            Domain.Session sample = Mock.Session.One;
            DateTimeOffset expected = Time.Fetch.Add(Domain.Session.ExpirationDuration);
            sample.ExtendExpiration(Time.Fetch);
            Assert.AreEqual(expected, sample.Expiration);
        }
    }
}
