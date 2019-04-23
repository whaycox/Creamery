using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Domain.Security.Tests
{
    [TestClass]
    public class Session : SecureObjectTemplate
    {
        [TestMethod]
        public void SessionIDIsBase64()
        {
            VerifyBase64(Security.Session.NewSessionID, Security.Session.SessionIdentifierLengthInBytes);
        }

        [TestMethod]
        public void DifferentSessionIDEachTime()
        {
            Assert.AreNotEqual(Security.Session.NewSessionID, Security.Session.NewSessionID);
        }

        [TestMethod]
        public void CanIncrementExpiration()
        {
            Security.Session sample = MockSession.One;
            DateTimeOffset expected = sample.Expiration.Add(Security.Session.ExpirationDuration);
            sample.IncrementExpiration();
            Assert.AreEqual(expected, sample.Expiration);
        }
    }
}
