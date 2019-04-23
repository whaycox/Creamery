using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Application.Security;
using Curds.Domain.Persistence.EFCore;

namespace Curds.Infrastructure.Security.Tests
{
    [TestClass]
    public class SecurityProvider : ISecurityTemplate<Security.SecurityProvider>
    {
        private MockProvider Persistence = null;

        private Security.SecurityProvider _obj = null;
        protected override Security.SecurityProvider TestObject => _obj;

        [TestInitialize]
        public void BuildProvider()
        {
            Persistence = new MockProvider();
            Persistence.Reset();
            _obj = new Security.SecurityProvider(Time, Persistence);
        }

        protected override void EmptyUsers() => Persistence.MockUsers.EmptyUsers();
    }
}
