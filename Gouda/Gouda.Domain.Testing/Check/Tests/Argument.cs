using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Check.Tests
{
    [TestClass]
    public class Argument : NamedEntityTemplate<Check.Argument>
    {
        protected override Check.Argument Sample => MockArgument.One;

        [TestMethod]
        public void ValueEquality() => TestStringChange((e, v) => e.Value = v);
    }
}
