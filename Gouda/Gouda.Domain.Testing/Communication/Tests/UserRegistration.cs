using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication.Tests
{
    [TestClass]
    public class UserRegistration : EntityTemplate<Communication.UserRegistration>
    {
        protected override Communication.UserRegistration TestObject => MockUserRegistration.Sample;

        [TestMethod]
        public void DefinitionIDEquality() => TestIntChange((e, v) => e.DefinitionID = v);

        [TestMethod]
        public void UserIDEquality() => TestIntChange((e, v) => e.UserID = v);

        [TestMethod]
        public void CronStringEquality() => TestStringChange((e, v) => e.CronString = v);
    }
}
