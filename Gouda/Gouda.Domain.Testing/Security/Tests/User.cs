using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Security.Tests
{
    [TestClass]
    public class User : NamedEntityTemplate<Security.User>
    {
        protected override Security.User TestObject => MockUser.One;

        [TestMethod]
        public void EmailEquality() => TestStringChange((e, v) => e.Email = v);

        [TestMethod]
        public void SaltEquality() => TestStringChange((e, v) => e.Salt = v);

        [TestMethod]
        public void PasswordEquality() => TestStringChange((e, v) => e.Password = v);
    }
}
