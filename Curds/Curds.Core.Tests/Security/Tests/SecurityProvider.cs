using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Security.Tests
{
    using Template;
    using Persistence.Mock;

    [TestClass]
    public class SecurityProvider : ISecurityTemplate<Implementation.SecurityProvider>
    {
        private Implementation.SecurityProvider _obj = null;
        protected override Implementation.SecurityProvider TestObject => _obj;

        [TestInitialize]
        public void BuildProvider()
        {
            _obj = new Implementation.SecurityProvider(MockTime, MockPersistence);
        }
    }
}
