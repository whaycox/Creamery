using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Communication
{
    public abstract class ContactTemplate<T> : NamedEntityTemplate<T> where T : Contact
    {
        [TestMethod]
        public void UserIDEquality() => TestIntChange((e, v) => e.UserID = v);

        [TestMethod]
        public void CronStringEquality() => TestStringChange((e, v) => e.CronString = v);
    }
}
