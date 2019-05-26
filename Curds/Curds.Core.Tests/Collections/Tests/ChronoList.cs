using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Collections.Tests
{
    [TestClass]
    public class ChronoList : Template.ChronoList<Implementation.ChronoList<int>>
    {
        private Implementation.ChronoList<int> _obj = new Implementation.ChronoList<int>();
        protected override Implementation.ChronoList<int> TestObject => _obj;
    }
}
