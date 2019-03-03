using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Application.Security;
using Gouda.Domain.Persistence;

namespace Gouda.Infrastructure.Security.Tests
{
    [TestClass]
    public class SecurityProvider : ISecurityTemplate<Security.SecurityProvider>
    {
        private MockPersistence Persistence = null;

        private Security.SecurityProvider _obj = null;
        protected override Security.SecurityProvider TestObject => _obj;

        [TestInitialize]
        public void BuildProvider()
        {
            Persistence = new MockPersistence(Cron);
            _obj = new Security.SecurityProvider(Time, Persistence);
        }

        [TestMethod]
        public void SaltIsCorrectFormat()
        {
            string saltToTest = TestObject.GenerateSalt();
            Assert.AreEqual(32, saltToTest.Length);
            Assert.IsTrue(Regex.IsMatch(saltToTest, "[0-9a-f]+"));
        }
    }
}
