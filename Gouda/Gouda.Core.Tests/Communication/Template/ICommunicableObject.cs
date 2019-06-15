using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Gouda.Communication.Template
{
    using Abstraction;
    using Domain;
    using Enumerations;

    public abstract class ICommunicableObject<T> : Test<T> where T : class, ICommunicableObject
    {
        private byte[] FetchByteArray() => TestObject.Content().ToArray();
        private BufferReader BuildReader() => new BufferReader(FetchByteArray());

        private string HashBytes(byte[] bytes)
        {
            using (SHA1 hasher = SHA1.Create())
            {
                byte[] hash = hasher.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [TestMethod]
        public void CanBuildStream()
        {
            byte[] content = FetchByteArray();
            Assert.AreEqual(ExpectedByteLength, content.Length);
            Assert.AreEqual(ExpectedShaHash, HashBytes(content));
        }
        protected abstract int ExpectedByteLength { get; }
        protected abstract string ExpectedShaHash { get; }

        [TestMethod]
        public void CanParse()
        {
            BufferReader reader = BuildReader();
            Assert.AreEqual(ExpectedType, reader.ParseType());
            ICommunicableObject parsed = Parser.Parse(reader);
            Assert.IsInstanceOfType(parsed, typeof(T));
            VerifyParsedObject(parsed as T);
        }
        protected abstract CommunicableType ExpectedType { get; }
        protected abstract IParser Parser { get; }
        protected abstract void VerifyParsedObject(T parsed);
    }
}
