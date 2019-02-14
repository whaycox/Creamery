using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Application.Security
{
    public abstract class IAuthenticatorTemplate<T> : TestTemplate<T> where T : IAuthenticator
    {
        private string TestEncryptedPassword => TestObject.EncryptPassword(Testing.TestSalt, Testing.TestPassword);

        [TestMethod]
        public void DifferentSaltsEachTime()
        {
            Assert.AreNotEqual(TestObject.GenerateSalt(), TestObject.GenerateSalt());
        }

        [TestMethod]
        public void NullOrEmptySaltThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword(null, Testing.TestPassword));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword(string.Empty, Testing.TestPassword));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword("   ", Testing.TestPassword));
        }

        [TestMethod]
        public void NullOrEmptyPasswordThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword(Testing.TestSalt, null));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword(Testing.TestSalt, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.EncryptPassword(Testing.TestSalt, "   "));
        }

        [TestMethod]
        public void DifferentSaltDifferentEncryptedPassword()
        {
            string toTest = TestObject.EncryptPassword(TestObject.GenerateSalt(), Testing.TestPassword);
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }

        [TestMethod]
        public void DifferentPasswordDifferentEncryptedPassword()
        {
            string toTest = TestObject.EncryptPassword(Testing.TestSalt, nameof(toTest));
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }
    }
}
