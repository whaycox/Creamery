using Curds.Domain.DateTimes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Infrastructure.Collections.Tests
{
    [TestClass]
    public class ChronoList : ChronoListTemplate<ChronoList<int>>
    {
        private ChronoList<int> _obj = new ChronoList<int>();
        protected override ChronoList<int> TestObject => _obj;
    }
}
