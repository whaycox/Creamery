using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Application.Security;

namespace Gouda.Infrastructure.Security.Tests
{
    [TestClass]
    public class SecurityProvider : ISecurityTemplate<Security.SecurityProvider>
    {
        protected override Security.SecurityProvider TestObject => new Security.SecurityProvider();

        [TestMethod]
        public void SaltIsCorrectFormat()
        {
            string saltToTest = TestObject.GenerateSalt();
            Assert.AreEqual(32, saltToTest.Length);
            Assert.IsTrue(Regex.IsMatch(saltToTest, "[0-9a-f]+"));
        }
    }
}
