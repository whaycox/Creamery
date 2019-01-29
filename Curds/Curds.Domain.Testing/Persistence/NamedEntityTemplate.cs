using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Persistence
{
    public abstract class NamedEntityTemplate<T> : EntityTemplate<T> where T : NamedEntity
    {
        [TestMethod]
        public void NameEquality() => TestStringChange((e, v) => e.Name = v);
    }
}
