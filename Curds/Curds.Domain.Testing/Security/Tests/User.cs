using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace Curds.Domain.Security.Tests
{
    [TestClass]
    public class User : SecureObjectTemplate
    {
        private string TestEncryptedPassword => Security.User.EncryptPassword(Testing.Salt, Testing.Password);

        [TestMethod]
        public void EncryptedPasswordIsBase64()
        {
            VerifyBase64(Security.User.EncryptPassword(Testing.Salt, Testing.Password), Security.User.PasswordLengthInBytes);
        }

        [TestMethod]
        public void GeneratedSaltIsBase64()
        {
            VerifyBase64(Security.User.NewSalt, Security.User.SaltLengthInBytes);
        }

        [TestMethod]
        public void DifferentSaltsEachTime()
        {
            Assert.AreNotEqual(Security.User.NewSalt, Security.User.NewSalt);
        }

        [TestMethod]
        public void NullOrEmptySaltThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword(null, Testing.Password));
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword(string.Empty, Testing.Password));
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword("   ", Testing.Password));
        }

        [TestMethod]
        public void NullOrEmptyPasswordThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword(Testing.Salt, null));
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword(Testing.Salt, string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => Security.User.EncryptPassword(Testing.Salt, "   "));
        }

        [TestMethod]
        public void DifferentSaltDifferentEncryptedPassword()
        {
            string toTest = Security.User.EncryptPassword(Security.User.NewSalt, Testing.Password);
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }

        [TestMethod]
        public void DifferentPasswordDifferentEncryptedPassword()
        {
            string toTest = Security.User.EncryptPassword(Testing.Salt, nameof(toTest));
            Assert.AreNotEqual(TestEncryptedPassword, toTest);
        }
    }
}
