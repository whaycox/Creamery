using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Security.Tests
{
    [TestClass]
    public class User : Template.SecureObject
    {
        private string TestEncryptedPassword => Domain.User.EncryptPassword(Testing.Salt, Testing.Password);

        [TestMethod]
        public void EncryptedPasswordIsBase64()
        {
            VerifyBase64(Domain.User.EncryptPassword(Testing.Salt, Testing.Password), Domain.User.PasswordLengthInBytes);
        }

        [TestMethod]
        public void GeneratedSaltIsBase64()
        {
            VerifyBase64(Domain.User.NewSalt, Domain.User.SaltLengthInBytes);
        }

        [TestMethod]
        public void DifferentSaltsEachTime()
        {
            Assert.AreNotEqual(Domain.User.NewSalt, Domain.User.NewSalt);
        }

        [TestMethod]
        public void NullOrEmptySaltThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword(null, Testing.Password));
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword(string.Empty, Testing.Password));
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword("   ", Testing.Password));
        }

        [TestMethod]
        public void NullOrEmptyPasswordThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword(Testing.Salt, null));
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword(Testing.Salt, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => Domain.User.EncryptPassword(Testing.Salt, "   "));
        }

        [TestMethod]
        public void DifferentSaltDifferentEncryptedPassword()
        {
            string toTest = Domain.User.EncryptPassword(Domain.User.NewSalt, Testing.Password);
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }

        [TestMethod]
        public void DifferentPasswordDifferentEncryptedPassword()
        {
            string toTest = Domain.User.EncryptPassword(Testing.Salt, nameof(toTest));
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }
    }
}
